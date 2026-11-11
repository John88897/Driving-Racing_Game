using System.Collections;
using UnityEngine;
using TMPro;

public class LapTimeManager : MonoBehaviour
{
    public static int MinuteCount;
    public static int SecondCount;
    public static float MilliCount;
    public static string MilliDisplay;

    [Header("Timer Delay")]
    public float countdownDelay = 3f;
    public bool canCount = false;

    [Header("UI Text References")]
    public TMP_Text MinuteBox;
    public TMP_Text SecondBox;
    public TMP_Text MilliBox;

    void Start()
    {
        // Reset timer values
        MinuteCount = 0;
        SecondCount = 0;
        MilliCount = 0f;

        // Display initial 00:00.0 on UI
        UpdateTimerDisplay();

        // Start delay coroutine
        StartCoroutine(StartTimerAfterDelay());
    }

    IEnumerator StartTimerAfterDelay()
    {
        canCount = false;
        yield return new WaitForSeconds(countdownDelay);
        canCount = true;
    }

    void Update()
    {
        // Block counting until the 3-second delay has passed
        if (!canCount) return;

        MilliCount += Time.deltaTime * 10f;

        if (MilliCount >= 10f)
        {
            MilliCount -= 10f;
            SecondCount += 1;
        }

        if (SecondCount >= 60)
        {
            SecondCount = 0;
            MinuteCount += 1;
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        if (MilliBox != null)
        {
            MilliDisplay = Mathf.FloorToInt(MilliCount).ToString();
            MilliBox.text = MilliDisplay;
        }

        if (SecondBox != null)
        {
            SecondBox.text = (SecondCount <= 9 ? "0" : "") + SecondCount + ".";
        }

        if (MinuteBox != null)
        {
            MinuteBox.text = (MinuteCount <= 9 ? "0" : "") + MinuteCount + ":";
        }
    }
}
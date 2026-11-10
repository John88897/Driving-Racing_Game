using UnityEngine;
using TMPro;

public class LapTimeManager : MonoBehaviour
{
    public static int MinuteCount;
    public static int SecondCount;
    public static float MilliCount;
    public static string MilliDisplay;

    [Header("UI Text References")]
    public TMP_Text MinuteBox;
    public TMP_Text SecondBox;
    public TMP_Text MilliBox;

    void Start()
    {
        // Reset timer when the scene or race starts
        MinuteCount = 0;
        SecondCount = 0;
        MilliCount = 0f;
    }

    void Update()
    {
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

        MilliDisplay = Mathf.FloorToInt(MilliCount).ToString();
        MilliBox.text = MilliDisplay;

        if (SecondCount <= 9)
        {
            SecondBox.text = "0" + SecondCount + ".";
        }
        else
        {
            SecondBox.text = SecondCount + ".";
        }

        if (MinuteCount <= 9)
        {
            MinuteBox.text = "0" + MinuteCount + ":";
        }
        else
        {
            MinuteBox.text = MinuteCount + ":";
        }
    }
}
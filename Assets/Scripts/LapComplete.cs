using UnityEngine;
using TMPro;

public class LapComplete : MonoBehaviour
{
    [Header("Trigger References")]
    public GameObject LapCompleteTrig;
    public GameObject HalfLapTrig;

    [Header("Best / Last Lap UI Displays")]
    public TMP_Text MinuteDisplay;
    public TMP_Text SecondDisplay;
    public TMP_Text MilliDisplay;

    public GameObject LapTimeBox;

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger if the colliding object is the player car
        if (!other.CompareTag("Player") && !other.name.Contains("Car")) return;

        // 1. Format and display the completed lap time
        if (MinuteDisplay != null)
        {
            MinuteDisplay.text = (LapTimeManager.MinuteCount <= 9 ? "0" : "") + LapTimeManager.MinuteCount + ":";
        }

        if (SecondDisplay != null)
        {
            SecondDisplay.text = (LapTimeManager.SecondCount <= 9 ? "0" : "") + LapTimeManager.SecondCount + ".";
        }

        if (MilliDisplay != null)
        {
            MilliDisplay.text = Mathf.FloorToInt(LapTimeManager.MilliCount).ToString();
        }

        // 2. Reset the running timer
        LapTimeManager.MinuteCount = 0;
        LapTimeManager.SecondCount = 0;
        LapTimeManager.MilliCount = 0f;

        // 3. Toggle triggers for the next lap sequence
        if (HalfLapTrig != null) HalfLapTrig.SetActive(true);
        if (LapCompleteTrig != null) LapCompleteTrig.SetActive(false);
    }
}
using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public TMP_Text countdownText;
    public Animator animator;

    void Start()
    {
        StartCoroutine(RunCountdown());
    }

    IEnumerator RunCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            animator.Play("CountdownUIPulse", -1, 0f); // Replays the spin/pulse from frame 0
            yield return new WaitForSeconds(1f);
        }
        
        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
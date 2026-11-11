using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public float timeLimit = 90f;

    [Header("UI (drag from Canvas)")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI statusText;
    public GameObject restartButton;   // optional: hidden until game over

    float timeLeft;
    bool gameOver;

    void Start()
    {
        Time.timeScale = 1f;
        timeLeft = timeLimit;
        gameOver = false;

        if (statusText != null)
            statusText.text = "Finish one lap before time runs out and avoid the police!";
        if (restartButton != null)
            restartButton.SetActive(false);
    }

    void Update()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);

        if (timeLeft <= 0f)
            Lose("TIME'S UP! You lose.");
    }

    public void Win()
    {
        if (gameOver) return;
        EndGame("YOU WIN! Time left: " + Mathf.CeilToInt(timeLeft));
    }

    public void Lose(string message = "TIME'S UP! You lose.")
    {
        if (gameOver) return;
        EndGame(message);
    }

    void EndGame(string message)
    {
        gameOver = true;
        if (statusText != null) statusText.text = message;
        if (restartButton != null) restartButton.SetActive(true);
        Time.timeScale = 0f;   // freezes the game
    }

    // Hook this to the Restart button's OnClick
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
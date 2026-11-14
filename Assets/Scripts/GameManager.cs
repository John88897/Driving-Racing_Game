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
    public GameObject restartButton;

    [Header("Status Colors")]
    public Color infoColor       = Color.white;                       // start instructions
    public Color checkpointColor = new Color(1f, 0.85f, 0.2f);        // yellow
    public Color warningColor    = new Color(1f, 0.55f, 0f);          // orange
    public Color winColor        = new Color(0.2f, 1f, 0.3f);         // green
    public Color loseColor       = new Color(1f, 0.25f, 0.25f);       // red

    [HideInInspector] public bool halfwayPassed;

    float timeLeft;
    bool gameOver;

    void Start()
    {
        Time.timeScale = 1f;
        timeLeft = timeLimit;
        gameOver = false;
        halfwayPassed = false;

        ShowStatus("Finish one lap in time and avoid the police!", infoColor);
        if (restartButton != null) restartButton.SetActive(false);
    }

    void Update()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
            // timer turns red in the last 10 seconds
            timerText.color = timeLeft <= 10f ? loseColor : Color.white;
        }

        if (timeLeft <= 0f)
            Lose("TIME'S UP! You lose.");
    }

    // Sets the status message and its colour together
    void ShowStatus(string message, Color color)
    {
        if (statusText == null) return;
        statusText.text = message;
        statusText.color = color;
    }

    public void ReachCheckpoint()
    {
        if (gameOver || halfwayPassed) return;
        halfwayPassed = true;
        ShowStatus("Checkpoint reached! Head to the finish line.", checkpointColor);
    }

    public void ReachFinish()
    {
        if (gameOver) return;

        if (!halfwayPassed)
        {
            ShowStatus("Pass the halfway checkpoint first!", warningColor);
            return;
        }

        Win();
    }

    public void Win()
    {
        if (gameOver) return;
        EndGame("CONGRATULATIONS! \n Time left: " + Mathf.CeilToInt(timeLeft), winColor);
    }

    public void Lose(string message = "TIME'S UP! You lose.")
    {
        if (gameOver) return;
        EndGame(message, loseColor);
    }

    void EndGame(string message, Color color)
    {
        gameOver = true;
        ShowStatus(message, color);
        if (restartButton != null) restartButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
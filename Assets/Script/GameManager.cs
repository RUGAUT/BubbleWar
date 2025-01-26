using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public Text bestScoreText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public Button restartButton;
    public Button menuButton;
    public Button pauseButton;
    public Text pauseButtonText;

    public GameObject animationObject; // GameObject contenant l'Animator
    public float animationDuration = 1.5f; // Durée pendant laquelle l'animation est jouée avant le Game Over

    private int score = 0;
    private float elapsedTime = 0f;
    private bool isGameOver = false;
    private bool isPaused = false;
    private int bestScore = 0;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (bestScoreText != null)
        {
            bestScoreText.text = $"Best Score: {bestScore}";
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (animationObject != null)
        {
            animationObject.SetActive(false); // Assurez-vous que l'animation est désactivée au début
        }

        Time.timeScale = 1f;

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (menuButton != null)
            menuButton.onClick.AddListener(ReturnToMenu);

        if (pauseButton != null)
            pauseButton.onClick.AddListener(TogglePause);
    }

    void Update()
    {
        if (isGameOver || isPaused) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timeText.text = $"Time: {minutes:D2}:{seconds:D2}";
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return; // Éviter de déclencher plusieurs fois
        isGameOver = true;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        if (animationObject != null)
        {
            animationObject.SetActive(true); // Activer l'objet pour jouer l'animation
        }

        // Lancer la coroutine pour attendre avant d'afficher le panneau Game Over
        StartCoroutine(ShowGameOverPanelAfterAnimation());
    }

    private System.Collections.IEnumerator ShowGameOverPanelAfterAnimation()
    {
        yield return new WaitForSeconds(animationDuration); // Attendre la durée de l'animation

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Afficher le panneau Game Over
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = $"Best Score: {bestScore}";
        }

        Time.timeScale = 0f; // Mettre le jeu en pause
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }

        if (pauseButtonText != null)
        {
            pauseButtonText.text = isPaused ? "Resume" : "Pause";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}










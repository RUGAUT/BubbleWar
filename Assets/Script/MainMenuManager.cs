using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Text bestScoreText; // R�f�rence au texte UI pour afficher le meilleur score

    void Start()
    {
        // Charger le meilleur score sauvegard�
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // Mettre � jour le texte du meilleur score
        if (bestScoreText != null)
        {
            bestScoreText.text = $"Best Score: {bestScore}";
        }
    }
}


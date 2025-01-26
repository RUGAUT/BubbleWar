using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Text bestScoreText; // Référence au texte UI pour afficher le meilleur score

    void Start()
    {
        // Charger le meilleur score sauvegardé
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // Mettre à jour le texte du meilleur score
        if (bestScoreText != null)
        {
            bestScoreText.text = $"Best Score: {bestScore}";
        }
    }
}


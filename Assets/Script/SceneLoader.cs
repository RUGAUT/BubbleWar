using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes

public class SceneLoader : MonoBehaviour
{
    // Méthode publique pour charger une scène
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Méthode pour quitter le jeu
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Le jeu a été quitté."); // Utile en mode éditeur (ne quitte pas réellement l'éditeur)
    }
}


using UnityEngine;
using UnityEngine.SceneManagement; // N�cessaire pour g�rer les sc�nes

public class SceneLoader : MonoBehaviour
{
    // M�thode publique pour charger une sc�ne
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // M�thode pour quitter le jeu
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Le jeu a �t� quitt�."); // Utile en mode �diteur (ne quitte pas r�ellement l'�diteur)
    }
}


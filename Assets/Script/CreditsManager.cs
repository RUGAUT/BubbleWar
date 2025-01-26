using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    // Retourne au menu principal
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene"); // Remplacez par le nom exact de votre scène de menu principal
    }

    // Quitte le jeu
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu !");
        Application.Quit();
    }
}


using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        // Trouver le GameManager dans la sc�ne
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si l'eau touche une Death Zone
        if (other.CompareTag("DeathZone"))
        {
            // D�clencher le Game Over
            gameManager.TriggerGameOver();
        }
    }
}


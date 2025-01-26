using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;

    public float riseAmount;
    public float maxHeight;
    public GameObject Water;

    void Start()
    {
        // Trouver le GameManager dans la scène
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si l'objet touché est un ennemi
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            // Récupérer le script Enemy attaché à l'ennemi
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Ajouter les points correspondants au score
                gameManager.AddScore(enemy.scoreValue);

                // Appeler la méthode pour lancer l'animation de destruction de l'ennemi
                enemy.TriggerDestructionAnimation();
            }

            // Détruire l'ennemi après la collision (facultatif, car la destruction sera gérée par l'animation)
            // Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("WaterDecrease"))
        {
            Debug.Log("FFFFFF");
            FillWater waterScript = Water.gameObject.GetComponent<FillWater>();
            if (waterScript != null)
            {
                Debug.Log("PAS NULL");
                waterScript.UnFill(riseAmount, maxHeight);
            }

            //TriggerDestructionAnimation();
        }
    }
}




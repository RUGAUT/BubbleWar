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
        // Trouver le GameManager dans la sc�ne
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si l'objet touch� est un ennemi
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            // R�cup�rer le script Enemy attach� � l'ennemi
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Ajouter les points correspondants au score
                gameManager.AddScore(enemy.scoreValue);

                // Appeler la m�thode pour lancer l'animation de destruction de l'ennemi
                enemy.TriggerDestructionAnimation();
            }

            // D�truire l'ennemi apr�s la collision (facultatif, car la destruction sera g�r�e par l'animation)
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




using UnityEngine;
using System.Collections;

public class WaterCollisionHandler : MonoBehaviour
{
    public GameObject otherObject; // L'autre objet à détecter pour la collision
    public float fallAmount = 0.2f; // La quantité de descente que l'on veut appliquer
    public float dynamicTime = 2f; // Temps pendant lequel le Rigidbody2D sera en mode dynamique

    private Rigidbody2D parentRigidbody;

    void Start()
    {
        // Récupérer le Rigidbody2D du parent
        parentRigidbody = transform.parent.GetComponent<Rigidbody2D>();

        // Vérifier si le Rigidbody2D existe et est initialisé
        if (parentRigidbody != null)
        {
            parentRigidbody.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log("Rigidbody2D trouvé et défini sur Kinematic.");
        }
        else
        {
            Debug.LogError("Aucun Rigidbody2D trouvé sur le parent.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si l'objet avec le tag "Player" entre en collision avec l'objet
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision avec le joueur détectée.");

            // Si le Rigidbody2D existe, le passer en mode dynamique et faire descendre l'objet
            if (parentRigidbody != null)
            {
                parentRigidbody.bodyType = RigidbodyType2D.Dynamic;
                Debug.Log("Rigidbody2D passé en mode dynamique.");

                // Appliquer une force descendante (simuler la descente)
                parentRigidbody.linearVelocity = new Vector2(parentRigidbody.linearVelocity.x, -fallAmount);

                // Remettre le Rigidbody2D en mode Kinematic après quelques secondes
                StartCoroutine(ResetToKinematic());
            }
        }
    }

    // Coroutine pour remettre le Rigidbody2D en mode Kinematic après un délai
    private IEnumerator ResetToKinematic()
    {
        // Attendre la durée spécifiée
        yield return new WaitForSeconds(dynamicTime);

        // Remettre le Rigidbody2D en mode Kinematic
        if (parentRigidbody != null)
        {
            parentRigidbody.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log("Rigidbody2D remis en mode Kinematic après délai.");
        }
    }
}






using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour
{
    public float health = 50f; // Vie du bloc
    public float riseAmount = 0.5f; // Montée par collision
    public float maxHeight = 5f; // Hauteur maximale que l'objet "Eau" peut atteindre
    public float fallAmount = 0.2f; // Descente par collision avec le tag "Player"
    public float fillSpeed = 2f; // Vitesse de remplissage (plus c'est grand, plus c'est rapide)
    public float destructionDelay = 1f; // Délai avant la destruction (pour permettre à l'animation de jouer)
    public AudioClip destructionSound; // Clip sonore de la destruction

    private Animator animator; // Référence à l'Animator
    private bool isFalling = false; // Indicateur de chute (si l'objet descend)
    private AudioSource audioSource; // Référence à l'AudioSource

    void Start()
    {
        // Récupère le composant Animator sur l'objet
        animator = GetComponent<Animator>();

        // Ajouter ou récupérer un composant AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        // Si l'impact est suffisamment fort, infliger des dégâts à l'objet
        if (impactForce > 5f)
        {
            health -= impactForce;

            if (health <= 0)
            {
                TriggerDestructionAnimation();
            }
        }

        // Si l'objet avec lequel on entre en collision a le tag "Eau"
        if (collision.gameObject.CompareTag("Eau"))
        {
            FillWater waterScript = collision.gameObject.GetComponent<FillWater>();
            if (waterScript != null)
            {
                waterScript.Fill(riseAmount, maxHeight);
            }

            TriggerDestructionAnimation();
        }



        // Si l'objet avec lequel on entre en collision a le tag "Player" et le layer "EauBas"
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == LayerMask.NameToLayer("EauBas"))
        {
           // StartFalling();
        }
    }

    private void TriggerDestructionAnimation()
    {
       /* if (animator != null)
        {
            animator.SetTrigger("Destroy");
        }*/

        // Jouer l'effet sonore
        if (destructionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(destructionSound);
        }

        // Détruire l'objet après un délai
        Destroy(gameObject, destructionDelay);
    }
    /*
        private void StartFalling()
        {
            if (!isFalling)
            {
                isFalling = true;
                StartCoroutine(FallCoroutine());
            }
        }

        private IEnumerator FallCoroutine()
        {
            Vector3 originalPosition = transform.position;
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y - fallAmount, transform.position.z);

            float elapsedTime = 0f;
            float fallDuration = 1f;

            while (elapsedTime < fallDuration)
            {
                transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / fallDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            isFalling = false;
        }
    */
}











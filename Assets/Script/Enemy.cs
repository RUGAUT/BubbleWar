using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int scoreValue = 1; // Points apportés par cet ennemi
    private Animator animator; // Référence à l'Animator
    public float destructionDelay = 1f; // Délai avant la destruction de l'ennemi après l'animation
    public AudioClip destructionSound; // Son joué lors de la destruction

    private AudioSource audioSource; // Référence à l'AudioSource

    void Start()
    {
        // Récupère le composant Animator de l'ennemi
        animator = GetComponent<Animator>();

        // Ajouter ou récupérer un composant AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Fonction pour lancer l'animation de destruction avant de détruire l'ennemi
    public void TriggerDestructionAnimation()
    {
        if (animator != null)
        {
            // Déclenche l'animation de destruction
            animator.SetTrigger("Destroy");
        }

        // Jouer le son même si l'objet est désactivé
        PlayDestructionSound();

        // Détruire l'objet après un délai
        Destroy(gameObject, destructionDelay);
    }

    private void PlayDestructionSound()
    {
        if (destructionSound != null)
        {
            // Crée une source audio temporaire pour jouer le son
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);
        }
    }
}



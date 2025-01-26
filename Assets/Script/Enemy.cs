using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int scoreValue = 1; // Points apport�s par cet ennemi
    private Animator animator; // R�f�rence � l'Animator
    public float destructionDelay = 1f; // D�lai avant la destruction de l'ennemi apr�s l'animation
    public AudioClip destructionSound; // Son jou� lors de la destruction

    private AudioSource audioSource; // R�f�rence � l'AudioSource

    void Start()
    {
        // R�cup�re le composant Animator de l'ennemi
        animator = GetComponent<Animator>();

        // Ajouter ou r�cup�rer un composant AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Fonction pour lancer l'animation de destruction avant de d�truire l'ennemi
    public void TriggerDestructionAnimation()
    {
        if (animator != null)
        {
            // D�clenche l'animation de destruction
            animator.SetTrigger("Destroy");
        }

        // Jouer le son m�me si l'objet est d�sactiv�
        PlayDestructionSound();

        // D�truire l'objet apr�s un d�lai
        Destroy(gameObject, destructionDelay);
    }

    private void PlayDestructionSound()
    {
        if (destructionSound != null)
        {
            // Cr�e une source audio temporaire pour jouer le son
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);
        }
    }
}



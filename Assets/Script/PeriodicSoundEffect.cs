using UnityEngine;

public class PeriodicSoundEffect : MonoBehaviour
{
    public AudioSource audioSource1; // Premier AudioSource
    public AudioSource audioSource2; // Deuxième AudioSource
    public AudioClip soundEffect1; // Effet sonore du premier AudioSource
    public AudioClip soundEffect2; // Effet sonore du deuxième AudioSource
    private bool playFirstSound = true; // Variable pour alterner entre les sons

    void Start()
    {
        // Vérifie si les AudioSources sont attachés à l'objet
        if (audioSource1 == null)
        {
            audioSource1 = gameObject.AddComponent<AudioSource>();
        }
        if (audioSource2 == null)
        {
            audioSource2 = gameObject.AddComponent<AudioSource>();
        }

        // Désactive la lecture automatique pour ne pas jouer au démarrage
        audioSource1.playOnAwake = false;
        audioSource2.playOnAwake = false;
    }

    // Appelé par un événement dans l'animation
    public void PlaySoundEffect()
    {
        // Alterne entre les deux sons
        if (playFirstSound)
        {
            if (soundEffect1 != null)
            {
                audioSource1.PlayOneShot(soundEffect1);
            }
            else
            {
                Debug.LogWarning("Aucun effet sonore assigné au premier AudioSource !");
            }
        }
        else
        {
            if (soundEffect2 != null)
            {
                audioSource2.PlayOneShot(soundEffect2);
            }
            else
            {
                Debug.LogWarning("Aucun effet sonore assigné au deuxième AudioSource !");
            }
        }

        // Alterne la valeur de playFirstSound pour le prochain passage
        playFirstSound = !playFirstSound;
    }
}




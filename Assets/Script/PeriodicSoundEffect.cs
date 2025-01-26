using UnityEngine;

public class PeriodicSoundEffect : MonoBehaviour
{
    public AudioSource audioSource1; // Premier AudioSource
    public AudioSource audioSource2; // Deuxi�me AudioSource
    public AudioClip soundEffect1; // Effet sonore du premier AudioSource
    public AudioClip soundEffect2; // Effet sonore du deuxi�me AudioSource
    private bool playFirstSound = true; // Variable pour alterner entre les sons

    void Start()
    {
        // V�rifie si les AudioSources sont attach�s � l'objet
        if (audioSource1 == null)
        {
            audioSource1 = gameObject.AddComponent<AudioSource>();
        }
        if (audioSource2 == null)
        {
            audioSource2 = gameObject.AddComponent<AudioSource>();
        }

        // D�sactive la lecture automatique pour ne pas jouer au d�marrage
        audioSource1.playOnAwake = false;
        audioSource2.playOnAwake = false;
    }

    // Appel� par un �v�nement dans l'animation
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
                Debug.LogWarning("Aucun effet sonore assign� au premier AudioSource !");
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
                Debug.LogWarning("Aucun effet sonore assign� au deuxi�me AudioSource !");
            }
        }

        // Alterne la valeur de playFirstSound pour le prochain passage
        playFirstSound = !playFirstSound;
    }
}




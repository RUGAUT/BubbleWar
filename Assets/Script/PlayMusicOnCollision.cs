using UnityEngine;

public class PlayMusicOnTrigger : MonoBehaviour
{
    public AudioClip musicClip; // La musique � jouer
    public string targetTag; // Tag de l'objet � d�tecter pour jouer la musique
    private AudioSource audioSource;

    void Start()
    {
        // Ajoute ou r�cup�re un AudioSource attach� � l'objet
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false; // La musique ne doit pas jouer automatiquement
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // V�rifie si l'objet entrant a le tag cible et qu'un clip audio est d�fini
        if (collision.gameObject.CompareTag(targetTag) && musicClip != null)
        {
            PlayMusic();
        }
    }

    private void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }
}



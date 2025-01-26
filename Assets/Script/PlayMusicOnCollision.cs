using UnityEngine;

public class PlayMusicOnTrigger : MonoBehaviour
{
    public AudioClip musicClip; // La musique à jouer
    public string targetTag; // Tag de l'objet à détecter pour jouer la musique
    private AudioSource audioSource;

    void Start()
    {
        // Ajoute ou récupère un AudioSource attaché à l'objet
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false; // La musique ne doit pas jouer automatiquement
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet entrant a le tag cible et qu'un clip audio est défini
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



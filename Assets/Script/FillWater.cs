using UnityEngine;

public class FillWater : MonoBehaviour
{
    private float targetHeight; // Hauteur cible vers laquelle l'eau doit monter
    private bool isFilling; // Indique si l'eau est en train de monter
    public float fillSpeed = 2f; // Vitesse de montée de l'eau
    public float fallSpeed = 2f; // Vitesse de descente de l'eau

    [Header("Audio Settings")]
    public AudioClip fillSound; // Son joué lorsque l'eau monte
    private AudioSource audioSource; // Source audio pour jouer les sons

    [Header("Water Decrease Settings")]
    public float decreaseAmount = 0.2f; // Quantité de descente de l'eau par collision

    private void Start()
    {
        targetHeight = transform.position.y; // Initialiser à la hauteur actuelle

        // Initialiser l'AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (isFilling)
        {
            // Monter progressivement vers la hauteur cible
            float newY = Mathf.MoveTowards(transform.position.y, targetHeight, fillSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // Arrêter une fois la hauteur cible atteinte
            if (Mathf.Approximately(transform.position.y, targetHeight))
            {
                isFilling = false;
            }
        }
        else if (targetHeight < transform.position.y)
        {
            // Descendre progressivement vers la hauteur cible
            float newY = Mathf.MoveTowards(transform.position.y, targetHeight, fallSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    // Appelé par le script Destructible pour remplir l'eau
    public void Fill(float amount, float maxHeight)
    {
        float nextHeight = targetHeight + amount;

        // Si la prochaine hauteur dépasse la limite, la restreindre
        targetHeight = Mathf.Min(nextHeight, maxHeight);

        if (!isFilling)
        {
            // Jouer le son seulement si l'eau commence à monter
            if (fillSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fillSound);
            }
        }

        isFilling = true;
    }

    public void UnFill(float amount, float maxHeight)
    {
        float nextHeight = targetHeight - amount;

        // Si la prochaine hauteur dépasse la limite, la restreindre
        targetHeight = Mathf.Min(nextHeight, maxHeight);

        if (!isFilling)
        {
            // Jouer le son seulement si l'eau commence à monter
            if (fillSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fillSound);
            }
        }

        isFilling = true;
    }

    // Appelé pour faire descendre l'eau
    public void Decrease(float amount)
    {
        targetHeight -= amount;
        isFilling = false; // Assurer que l'eau descend instantanément (sans interpolation si non désiré)
    }

    // Gestion de la collision entre le joueur et l'objet ayant le tag "Water"
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si l'objet touché a le tag "Water"
        if (collision.gameObject.CompareTag("Water"))
        {
            // Vérifier si le joueur (tag "Player") est impliqué dans la collision
            if (collision.otherCollider.CompareTag("Player"))
            {
                Debug.Log("Le joueur a touché l'eau. Réduction de la hauteur.");
                Decrease(decreaseAmount); // Faire descendre l'eau
            }
        }
    }
}






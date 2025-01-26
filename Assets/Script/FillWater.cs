using UnityEngine;

public class FillWater : MonoBehaviour
{
    private float targetHeight; // Hauteur cible vers laquelle l'eau doit monter
    private bool isFilling; // Indique si l'eau est en train de monter
    public float fillSpeed = 2f; // Vitesse de mont�e de l'eau
    public float fallSpeed = 2f; // Vitesse de descente de l'eau

    [Header("Audio Settings")]
    public AudioClip fillSound; // Son jou� lorsque l'eau monte
    private AudioSource audioSource; // Source audio pour jouer les sons

    [Header("Water Decrease Settings")]
    public float decreaseAmount = 0.2f; // Quantit� de descente de l'eau par collision

    private void Start()
    {
        targetHeight = transform.position.y; // Initialiser � la hauteur actuelle

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

            // Arr�ter une fois la hauteur cible atteinte
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

    // Appel� par le script Destructible pour remplir l'eau
    public void Fill(float amount, float maxHeight)
    {
        float nextHeight = targetHeight + amount;

        // Si la prochaine hauteur d�passe la limite, la restreindre
        targetHeight = Mathf.Min(nextHeight, maxHeight);

        if (!isFilling)
        {
            // Jouer le son seulement si l'eau commence � monter
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

        // Si la prochaine hauteur d�passe la limite, la restreindre
        targetHeight = Mathf.Min(nextHeight, maxHeight);

        if (!isFilling)
        {
            // Jouer le son seulement si l'eau commence � monter
            if (fillSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fillSound);
            }
        }

        isFilling = true;
    }

    // Appel� pour faire descendre l'eau
    public void Decrease(float amount)
    {
        targetHeight -= amount;
        isFilling = false; // Assurer que l'eau descend instantan�ment (sans interpolation si non d�sir�)
    }

    // Gestion de la collision entre le joueur et l'objet ayant le tag "Water"
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // V�rifier si l'objet touch� a le tag "Water"
        if (collision.gameObject.CompareTag("Water"))
        {
            // V�rifier si le joueur (tag "Player") est impliqu� dans la collision
            if (collision.otherCollider.CompareTag("Player"))
            {
                Debug.Log("Le joueur a touch� l'eau. R�duction de la hauteur.");
                Decrease(decreaseAmount); // Faire descendre l'eau
            }
        }
    }
}






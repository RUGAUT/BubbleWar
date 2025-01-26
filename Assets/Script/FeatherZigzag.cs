using UnityEngine;

public class FeatherZigzagBetweenPoints : MonoBehaviour
{
    public Transform pointA;          // Premier point (gauche)
    public Transform pointB;          // Deuxième point (droite)
    public float fallSpeed = 2f;      // Vitesse de chute verticale
    public float zigzagSpeed = 3f;    // Vitesse du zigzag (oscillation)
    public bool randomizeStart = true; // Démarre avec une phase aléatoire

    private float startZigzagPhase;   // Phase initiale pour l'oscillation sinusoidale

    void Start()
    {
        // Vérifie que les points A et B sont définis
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Assignez les points A et B dans l'inspecteur.");
            enabled = false;
            return;
        }

        // Initialise la phase de zigzag pour éviter que tous les objets tombent de la même manière
        startZigzagPhase = randomizeStart ? Random.Range(0f, Mathf.PI * 2f) : 0f;
    }

    void Update()
    {
        // Calcul du déplacement vertical
        Vector3 fallMovement = Vector3.down * fallSpeed * Time.deltaTime;

        // Calcule la position horizontale oscillante entre pointA et pointB
        float leftLimit = pointA.position.x;
        float rightLimit = pointB.position.x;

        // Oscillation sinusoidale pour bouger entre les deux points
        float t = (Mathf.Sin(Time.time * zigzagSpeed + startZigzagPhase) + 1f) / 2f; // Normalisé entre 0 et 1
        float xPosition = Mathf.Lerp(leftLimit, rightLimit, t);

        // Met à jour la position de l'objet
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z) + fallMovement;
    }
}


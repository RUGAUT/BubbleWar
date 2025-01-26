using UnityEngine;
using System.Collections;

public class SlingshotMobile : MonoBehaviour
{
    public Transform pivotPoint;           // Point d'attache de la bande élastique (le point qui bouge)
    public LineRenderer rubberBand;        // Ligne représentant la bande
    public LineRenderer trajectoryLine;    // Ligne représentant la trajectoire
    public Rigidbody2D bird;               // L'objet à lancer
    public Transform respawnPoint;         // Position de réapparition de l'objet
    public float maxLength = 3f;           // Longueur maximale de la bande
    public float launchPower = 10f;        // Puissance du tir
    public float respawnDelay = 3f;        // Temps avant de réapparaître
    public float minWidth = 0.05f;         // Largeur minimale de la trajectoire
    public float maxWidth = 0.2f;          // Largeur maximale de la trajectoire
    public float trajectoryMultiplier = 2f; // Facteur de longueur de la trajectoire (ajuste la portée)
    private bool isDragging = false;       // Indique si l'utilisateur est en train de tirer l'oiseau

    // Rendre les indices publics pour les définir dans l'Inspector
    public Vector3 point0Position;         // Point fixe gauche
    public Vector3 point2Position;         // Point fixe droite (ne bouge pas)

    public AudioSource stretchSound;       // AudioSource pour jouer le son d'étirement

    private bool stretchSoundPlaying = false; // Indique si le son d'étirement est en train de jouer

    void Start()
    {
        // Initialisation des LineRenderers
        rubberBand.positionCount = 3; // Trois points au lieu de deux
        trajectoryLine.positionCount = 2;
        trajectoryLine.enabled = false; // Masque la ligne de trajectoire au début
        ResetBird(); // Réinitialise la position de l'objet
    }

    void Update()
    {
        if (Input.touchCount > 0) // Vérifie si l'écran est touché
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Vector3.Distance(touchPosition, bird.position) < 2f) // Distance max pour attraper l'oiseau
                    {
                        isDragging = true;
                        bird.isKinematic = true;
                        trajectoryLine.enabled = true; // Active la ligne de trajectoire
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector3 direction = touchPosition - pivotPoint.position;
                        float distance = direction.magnitude;

                        // Limite la distance à maxLength
                        if (distance > maxLength)
                        {
                            touchPosition = pivotPoint.position + direction.normalized * maxLength;
                        }

                        bird.position = touchPosition;

                        // Met à jour la bande élastique avec trois points
                        rubberBand.SetPosition(0, point0Position); // Point fixe gauche
                        rubberBand.SetPosition(1, bird.position); // Point central (pivot qui bouge)
                        rubberBand.SetPosition(2, point2Position); // Point fixe droite

                        // Calcule la direction et met à jour la ligne de trajectoire
                        Vector2 launchDirection = (Vector2)pivotPoint.position - (Vector2)bird.position;
                        Vector3 startPos = bird.position;
                        Vector3 endPos = (Vector3)bird.position + new Vector3(launchDirection.normalized.x * trajectoryMultiplier, launchDirection.normalized.y * trajectoryMultiplier, 0);

                        // Mise à jour de la trajectoire
                        trajectoryLine.SetPosition(0, startPos);
                        trajectoryLine.SetPosition(1, endPos);

                        // Jouer le son d'étirement si ce n'est pas déjà fait
                        if (!stretchSoundPlaying && stretchSound != null)
                        {
                            stretchSound.Play();
                            stretchSoundPlaying = true;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        isDragging = false;
                        bird.isKinematic = false;
                        Vector2 direction = (Vector2)pivotPoint.position - bird.position;
                        bird.linearVelocity = direction * launchPower;

                        // Désactive les lignes de trajectoire et de bande
                        rubberBand.positionCount = 0;
                        trajectoryLine.enabled = false;

                        // Arrête le son d'étirement
                        if (stretchSound != null)
                        {
                            stretchSound.Stop();
                            stretchSoundPlaying = false;
                        }

                        // Lance la coroutine pour faire réapparaître l'objet
                        StartCoroutine(RespawnBird());
                    }
                    break;
            }
        }
        else if (stretchSoundPlaying && stretchSound != null)
        {
            // Arrête le son si l'utilisateur a lâché l'écran
            stretchSound.Stop();
            stretchSoundPlaying = false;
        }
    }

    private IEnumerator RespawnBird()
    {
        yield return new WaitForSeconds(respawnDelay); // Attend quelques secondes
        ResetBird();
    }

    private void ResetBird()
    {
        // Réinitialise la position et l'état de l'objet
        bird.position = respawnPoint.position;
        bird.linearVelocity = Vector2.zero;
        bird.isKinematic = true;

        // Réinitialise la bande élastique
        rubberBand.positionCount = 3; // Trois points
        rubberBand.SetPosition(0, point0Position); // Réinitialisation du point fixe gauche
        rubberBand.SetPosition(1, bird.position); // Réinitialisation du point central (pivot)
        rubberBand.SetPosition(2, point2Position); // Réinitialisation du point fixe droit
    }
}





















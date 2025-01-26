using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 50f; // Vitesse du défilement

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}


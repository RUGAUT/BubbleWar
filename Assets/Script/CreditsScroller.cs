using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 50f; // Vitesse du d�filement

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}


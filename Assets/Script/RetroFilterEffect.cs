using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RetroFilterEffect : MonoBehaviour
{
    public Material retroFilterMaterial; // Assurez-vous de définir le matériel avec votre Shader
    public float noiseScale = 10f;       // Échelle du bruit
    public float speed = 1f;             // Vitesse de l'animation

    void Update()
    {
        if (retroFilterMaterial != null)
        {
            // Passe les paramètres au Shader
            retroFilterMaterial.SetFloat("_NoiseScale", noiseScale);
            retroFilterMaterial.SetFloat("_Speed", speed);
            retroFilterMaterial.SetFloat("_Time", Time.time);
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (retroFilterMaterial != null)
        {
            Graphics.Blit(src, dest, retroFilterMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}


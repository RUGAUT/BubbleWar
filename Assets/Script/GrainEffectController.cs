using UnityEngine;

public class GrainEffectController : MonoBehaviour
{
    public Material grainMaterial; // Matériau avec le shader
    public float speed = 0.1f; // Vitesse du décalage du grain

    private Vector2 offset = Vector2.zero;

    void Update()
    {
        if (grainMaterial != null)
        {
            // Modifier le décalage du grain pour l'animer
            offset += new Vector2(speed, speed) * Time.deltaTime;
            grainMaterial.SetVector("_Offset", new Vector4(offset.x, offset.y, 0, 0));
        }
    }
}


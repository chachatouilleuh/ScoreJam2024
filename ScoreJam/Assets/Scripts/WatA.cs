using UnityEngine;

public class WatA : MonoBehaviour
{
    // Vitesse de rotation autour de l'axe Y
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotation autour de l'axe Y en fonction de la vitesse de rotation
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
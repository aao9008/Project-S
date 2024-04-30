using UnityEngine;
using UnityEngine.UI;

public class LoadingSpinner : MonoBehaviour
{
    public float rotationSpeed = 180f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the RectTransform of the Image component
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}

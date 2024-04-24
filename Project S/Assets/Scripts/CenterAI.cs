using UnityEngine;

public class CenterAI : MonoBehaviour
{
    public float sphereSize = 1.0f; // Adjust this value as needed

    void Start()
    {
        // Calculate screen center
        float screenCenterX = Screen.width / 2.0f;
        float screenCenterY = Screen.height / 2.0f;

        // Calculate sphere position
        float spherePosX = screenCenterX;
        float spherePosY = sphereSize / 2.0f; // Offset by half of the sphere's size
        float spherePosZ = 0.65f; // Adjust the distance from the camera as needed

        // Set sphere position
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(spherePosX, screenCenterY - sphereSize / 2.0f, spherePosZ));
    }
}


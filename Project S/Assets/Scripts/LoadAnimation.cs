using UnityEngine;

public class LoadAnimation : MonoBehaviour
{
    // Reference to the main camera
    private Camera mainCamera;

    // Distance from the camera
    private float distanceFromCamera = 0.6f; // Adjust as needed
    private float verticalOffset = 0.08f; // Adjust as needed
    private float horizontalOffset = -0.05f; // Adjust as needed

    void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }
    }

    void Update()
    {
        // Ensure the main camera reference is valid
        if (mainCamera == null)
            return;

        // Calculate the center of the screen in screen space
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, distanceFromCamera);

        // Convert screen space center to world space point
        Vector3 centerInWorld = mainCamera.ScreenToWorldPoint(screenCenter);

        // Apply vertical offset
        centerInWorld -= mainCamera.transform.up * verticalOffset;

        // Apply horizontal offset
        centerInWorld += mainCamera.transform.right * horizontalOffset;

        // Set the position of the slate to be in front of the camera
        transform.position = centerInWorld;

        // Rotate the slate to face the camera
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position, Vector3.up);
    }
}
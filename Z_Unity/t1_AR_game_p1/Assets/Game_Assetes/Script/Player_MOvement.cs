using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Player_MOvement : MonoBehaviour
{
    public ARSessionOrigin arSessionOrigin;

    // Adjust this speed according to your preference
    public float movementSpeed = 2f;

    void Start()
    {
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
        if (arSessionOrigin == null)
        {
            Debug.LogError("ARSessionOrigin is not found.");
            enabled = false;
        }
    }

    void Update()
    {
        if (arSessionOrigin != null && arSessionOrigin.camera != null)
        {
            // Get the device's acceleration in AR space
            Vector3 deviceAcceleration = arSessionOrigin.camera.transform.TransformDirection(Input.acceleration);

            // Update the player's position based on device acceleration
            transform.Translate(deviceAcceleration * Time.deltaTime * movementSpeed, Space.World);

            // Get the device's rotation in AR space
            Quaternion deviceRotation = arSessionOrigin.camera.transform.rotation;

            // Convert the rotation to Euler angles and extract the Y-axis rotation
            float yRotation = deviceRotation.eulerAngles.y;

            // Update the player's rotation to match the device's rotation around the Y-axis
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}

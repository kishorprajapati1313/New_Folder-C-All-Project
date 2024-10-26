using UnityEngine;

public class MultiplayerCameraManager : MonoBehaviour
{
    public GameObject playerCameraPrefab; // Reference to the player camera prefab
    public Vector3 cameraOffset = new Vector3(0f, 0f, -10f); // Offset of the camera from the player

    // Method to create a new camera for a player
  public void CreatePlayerCamera(GameObject player)
{
    Debug.Log("Creating camera for player: " + player.name);

    // Create a new player camera for the player
    GameObject playerCamera = Instantiate(playerCameraPrefab, Vector3.zero, Quaternion.identity);

    if(playerCamera == null)
    {
        Debug.LogError("Camera prefab is null or not assigned.");
        return;
    }

    // Set the player camera's parent to the player GameObject
    playerCamera.transform.SetParent(player.transform);

    // Apply offset to the camera position
    playerCamera.transform.localPosition = cameraOffset;

    Debug.Log("Camera created and positioned.");
}

}

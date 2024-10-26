using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string targetPlayerName = "Player(Clone)"; // Name or identifier of the player to follow
    public float smoothTime = 0.3f; // Time it takes to reach the target position
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        GameObject targetPlayer = GameObject.Find(targetPlayerName);

        if (targetPlayer == null)
            return;

        // Calculate target position (ignore player's rotation)
        Vector3 targetPosition = new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, transform.position.z);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}

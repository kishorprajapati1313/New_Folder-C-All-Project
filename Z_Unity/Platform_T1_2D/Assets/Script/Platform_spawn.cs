using System.Collections;
using UnityEngine;

public class Platform_spawn : MonoBehaviour
{
    public GameObject platformPrefab; // Assign your platform prefab in the Inspector
    private Vector3 currentPlatformPosition = Vector3.zero; // Starting position
    private float platformSize = 1f;  // Adjust based on your platform size
    private Vector3[] directions = new Vector3[] {
        new Vector3(0, 0, 1),  // North
        new Vector3(1, 0, 0),  // East
        new Vector3(0, 0, -1), // South
        new Vector3(-1, 0, 0)  // West
    };
    private GameObject currentPlatform; // Store the current platform object
    private GameObject NewPlatform; // Store the current platform object
    private GameObject PrePlatform; // Store the current platform object

    private void Start()
    {
            currentPlatform = Instantiate(platformPrefab, currentPlatformPosition, Quaternion.identity);

        // Start the coroutine that spawns and destroys platforms
        StartCoroutine(SpawnAndDestroyPlatform());
    }

    IEnumerator SpawnAndDestroyPlatform()
    {
            

        while (true) // Keep running this loop to continuously spawn platforms
        {
             // Choose a random direction for the next platform
            Vector3 randomDirection = directions[Random.Range(0, directions.Length)];

            // Update the position for the next platform
            currentPlatformPosition += randomDirection * platformSize;

            // NewPlatform = Instantiate(platformPrefab, currentPlatformPosition, Quaternion.identity);


            // Wait for 0.5 seconds (or the desired time)
            // yield return new WaitForSeconds(0.5f);
            // Destroy the current platform
            Destroy(currentPlatform);

            // currentPlatform = NewPlatform;
        }
    }
}

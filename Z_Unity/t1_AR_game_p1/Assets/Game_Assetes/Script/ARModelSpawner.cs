using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARModelSpawner : MonoBehaviour
{
    public GameObject arModelPrefab; // Assign your AR model prefab in the Inspector
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();

        // Check if AR model prefab is assigned
        if (arModelPrefab == null)
        {
            Debug.LogError("AR Model Prefab not assigned!");
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Perform a raycast from the screen point where the user touched
            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                // Spawn AR model at the hit pose (position and rotation)
                Pose hitPose = hits[0].pose;
                SpawnARModel(hitPose.position, hitPose.rotation);
            }
        }
    }

    void SpawnARModel(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // Instantiate the AR model at the specified position and rotation
        GameObject arModelInstance = Instantiate(arModelPrefab, spawnPosition, spawnRotation);

      
    }
}

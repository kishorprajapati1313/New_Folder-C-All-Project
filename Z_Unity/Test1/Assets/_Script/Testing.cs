using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class Testing : NetworkBehaviour
{
    public Transform gunTransform; // Assign the gun's transform in the inspector

    private void Update()
    {
        if (IsOwner)
        {
            RotateGun();
        }
    }

    private void RotateGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - gunTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Send rotation to server
        RotateGunServerRpc(angle);
    }

    [ServerRpc]
    private void RotateGunServerRpc(float angle, NetworkConnection sender = null)
    {
        // Call the client RPC to update all clients
        RotateGunClientRpc(angle);
    }

    private void RotateGunClientRpc(float angle)
    {
        if (!IsOwner)
        {
            gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}

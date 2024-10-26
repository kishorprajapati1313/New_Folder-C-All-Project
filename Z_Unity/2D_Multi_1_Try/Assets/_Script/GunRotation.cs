using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class GunRotation : NetworkBehaviour 
{
    [Header("AimDirection")]
    public Transform Aim;
    public float aimDistance = 2f;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!base.IsOwner) return;

        AimPosition();
        RotateGun(Aim.position);
    }

    private void AimPosition()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the z position is zero

        // Calculate the direction from the gun's pivot to the mouse position
        Vector3 aimDirection = mousePosition - transform.position;

        // Normalize the aim direction and multiply by aimDistance to keep it within a fixed range
        aimDirection = aimDirection.normalized * aimDistance;

        // Set the aim Transform position
        Aim.position = transform.position + aimDirection;
    }

    private void RotateGun(Vector3 aimPosition)
    {
        
        // Calculate the angle between the gun's pivot and the aim position
        Vector3 aimDirection = aimPosition - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Send the rotation to the server
        CmdRotateGun(angle);
    }

    [ServerRpc]
    private void CmdRotateGun(float angle)
    {
        
        // Call the client RPC to update rotation for all clients
        RpcUpdateGunRotation(angle);
    }

    [ObserversRpc]
    private void RpcUpdateGunRotation(float angle)
    {
        if(!IsOwner){
            // Rotate the gun sprite to face the received aim direction around its pivot point
        transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }
}

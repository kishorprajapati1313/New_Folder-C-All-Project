using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class Rtest : NetworkBehaviour 
{
    public float movingSp = 2f;
    public float smoothRotationSpeed = 5f; // Speed of rotation interpolation

    private float horizontal;
    private float vertical;
    private Vector2 targetVelocity;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer; // Reference to the SpriteRenderer component

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (!base.IsOwner) return;
        
        // Get input from the player
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Calculate the target velocity based on input
        targetVelocity = new Vector2(horizontal, vertical).normalized * movingSp;

        // Update sprite rotation based on movement direction with smooth interpolation
        SmoothRotateSprite();
    }

    private void FixedUpdate()
    {
        if (!base.IsOwner) return;

        // Apply movement
        _rigidbody2D.velocity = targetVelocity;
    }

    private void SmoothRotateSprite()
    {
        // Calculate angle based on movement direction
        float angle = 0f;

        if (horizontal == 0 && vertical == 0)
        {
            angle = 0f; // No input
        }
        else if (Mathf.Abs(vertical) > Mathf.Abs(horizontal))
        {
            // Vertical movement
            angle = vertical > 0 ? 180f : 0f;
        }
        else
        {
            // Horizontal movement
            angle = horizontal > 0 ? 90f : -90f;
        }

        // Calculate the target rotation based on the calculated angle
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Smoothly interpolate between the current rotation and the target rotation
        _spriteRenderer.transform.rotation = Quaternion.Lerp(_spriteRenderer.transform.rotation, targetRotation, smoothRotationSpeed * Time.deltaTime);
    }
}

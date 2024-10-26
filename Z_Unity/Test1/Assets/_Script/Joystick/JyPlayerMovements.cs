using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class JyPlayerMovements : NetworkBehaviour 
{
    public float movingSp = 2f;
    public float smoothRotationSpeed = 5f; // Speed of rotation interpolation
    private Joystick joystick; // Reference to the Joystick component

    private float horizontal;
    private float vertical;
    private Vector3 targetPosition;
    private CharacterController _characterController;
    private SpriteRenderer _spriteRenderer; // Reference to the SpriteRenderer component

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Find and assign the joystick dynamically
        joystick = GameObject.Find("PlayerControll").GetComponent<Joystick>();
        if (joystick == null) {
            Debug.LogError("Joystick not found! Make sure its name is 'PlayerControll' in the scene.");
        }
    }
    
    private void Update()
    {
        if (!base.IsOwner || joystick == null) return;
        
        // Get input from the joystick
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        Debug.Log(horizontal);
        // Calculate the target position based on input
        targetPosition = new Vector3(-horizontal, vertical, 0) * movingSp;

        // Update sprite rotation based on movement direction with smooth interpolation
        SmoothRotateSprite();
    }

    private void FixedUpdate()
    {
        if (!base.IsOwner || joystick == null) return;

        // Apply movement
        _characterController.Move(targetPosition * Time.fixedDeltaTime);
    }

    private void SmoothRotateSprite()
    {
        Debug.Log("horizontal:" + horizontal * 100 + "vertical:" + vertical *100);
        // Calculate the angle based on joystick input
        float angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;

        if(horizontal > 0 && vertical > 0)
        {
            angle -= 180f;
        }
        else if(horizontal <0 && vertical < 0) {
            angle = 0f;
        } 
        else if(horizontal <0 && vertical > 0) {
            angle = 90f;
        } 
        else if(horizontal >0 && vertical < 0) {
            angle = -90f;
        } 

        // Adjust the angle to match the orientation of the sprite
        angle -= 0f; // Adjust based on the initial sprite orientation

        // Update the rotation of the sprite
        _spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


}

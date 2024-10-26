using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Android : MonoBehaviour
{
    #region Singleton class: Android
    public static Android Instance;

    void Awake (){
        if (Instance == null) {
            Instance = this;
        }
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManger>();
    }

    #endregion
    
    Camera cam;
    public Ball ball;
    [SerializeField] float pushForce = 4f;

    bool isDragging = false;
    bool hasDragged = false; // Flag to track if dragging has occurred
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;
    AudioManger audiomanager;


    void Start (){
        cam = Camera.main;
    }

    void Update (){
        if (!hasDragged && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch (assuming only single touch)
            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                OnDragStart ();
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
                hasDragged = true; // Set the flag to true after the first drag
                OnDragEnd ();
            }
        }

        if (isDragging) {
            OnDrag ();
        }
    }

    void OnDragStart (){
        startPoint = GetTouchWorldPosition();
    }

    void OnDrag (){
        endPoint = GetTouchWorldPosition();
        distance = Vector2.Distance (startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        // Draw the line
        Debug.DrawLine (startPoint, endPoint);
    }

    // Method to get touch position in world space
    Vector2 GetTouchWorldPosition(){
        Vector3 touchPos = Input.GetTouch(0).position;
        touchPos.z = -cam.transform.position.z; // Adjust Z to match the camera's position
        return cam.ScreenToWorldPoint(touchPos);
    }

    void OnDragEnd (){
        // Push the ball
        ball.ActiveRB ();
        ball.Push (force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // Debug.Log("I am Here");
            audiomanager.Playsfx(audiomanager.clash);
        }
    }
}

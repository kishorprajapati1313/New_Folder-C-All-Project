using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    #region Singleton class: GameManger

    public static GameManger Instance;

    void Awake (){
        if (Instance == null) {
            Instance = this;
        }
    }

    #endregion

    Camera cam;
    public Ball ball;
    public Trajectory trajectory;
    [SerializeField] float pushForce = 10f;

    bool isDragging = false;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    void Start (){
        cam = Camera.main;
    }

    void Update (){
        if (Input.GetMouseButtonDown (0)) {
            isDragging = true;
            OnDragStart ();
        }
        if (Input.GetMouseButtonUp (0)) {
            isDragging = false;
            OnDragEnd ();
        }

        if (isDragging) {
            OnDrag ();
        }
    }

    void OnDragStart (){
        startPoint = GetMouseWorldPosition();
        trajectory.Show();
    }

    void OnDrag (){
        endPoint = GetMouseWorldPosition();
        distance = Vector2.Distance (startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        // Draw the line
        Debug.DrawLine (startPoint, endPoint);

        trajectory.UpdateDots(ball.pos, force);
    }

    // Method to get mouse position in world space
    Vector2 GetMouseWorldPosition(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -cam.transform.position.z; // Adjust Z to match the camera's position
        return cam.ScreenToWorldPoint(mousePos);
    }


    void OnDragEnd (){
        // Push the ball
        ball.ActiveRB ();
        ball.Push (force);
        trajectory.Hide();
    }

}

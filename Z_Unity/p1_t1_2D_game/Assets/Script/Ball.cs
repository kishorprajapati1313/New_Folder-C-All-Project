using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public EdgeCollider2D edge;

    [HideInInspector] public Vector3 pos { get { return transform.position; } }
    AudioManger audiomanager;

    // Define a maximum speed for the ball
    public static float maxSpeed = 10f;
    // Define a deceleration factor
    public float decelerationFactor = 0.1f;

    // Boolean flag to track if the ball has been dragged already
    private bool hasBeenDragged = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        edge = GetComponent<EdgeCollider2D>();
        rb.isKinematic = true;
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManger>();
    }

    public void Push(Vector2 force)
        {
            if (!hasBeenDragged)
            {
                rb.isKinematic = false;
                rb.AddForce(force, ForceMode2D.Impulse);

                // Check if the current speed exceeds the maximum speed
                if (rb.velocity.magnitude > maxSpeed)
                {
                    // Apply deceleration to slow down the ball
                    rb.velocity *= (1 - decelerationFactor);
                }

                float rotationspeed = force.magnitude * 10f;
                float rotationdirection = Mathf.Sign(Vector3.Cross(force, Vector3.forward).z);
                rb.angularVelocity = rotationspeed * rotationdirection;

                hasBeenDragged = true; // Set the flag to true after the ball has been dragged once
            }

            if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;
                }
        }

    public void ActiveRB()
    {
        rb.isKinematic = false;
    }

    public void DesactiveRB()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // Debug.Log("I am Here");
            audiomanager.Playsfx(audiomanager.clash);
        }
        else if (other.gameObject.CompareTag("EditorOnly"))
        {
            Destroy(other.gameObject);
        }
    }
}

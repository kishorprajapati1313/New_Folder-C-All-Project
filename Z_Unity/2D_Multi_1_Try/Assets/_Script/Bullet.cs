using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class Bullet : NetworkBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    [SyncVar] // Synchronize the direction across the network
    private Vector2 direction;

    public int damage = 10; // Damage dealt by the bullet

  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
        if (IsServer)
        {
            rb.velocity = direction * speed;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        rb.velocity = direction * speed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if this is the server or the owner client
        if (!IsServer && !IsOwner) return;

        // Debug.Log("Bullet OnTriggerEnter2D");

        // Check if the bullet collided with the player's hitbox
        if (other.CompareTag("PlayerHitbox"))
        {
            // Debug.Log("Bullet hit player's hitbox");
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Destroy(gameObject, 0.01f);

            }
        }
        
        Destroy(gameObject, 0.01f);
        // Destroy the bullet on collision with any collider
        Despawn();
    }



    [ServerRpc ]
    private void Despawn()
    {
        Debug.Log("-------------------Hello");
            Destroy(gameObject, 0.01f);
        base.Despawn(gameObject);
    }


}

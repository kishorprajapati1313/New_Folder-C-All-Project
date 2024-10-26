using System.Collections;
using UnityEngine;
using FishNet.Object;

public class BulletShooting : NetworkBehaviour
{
    public GameObject bulletPrefab; // Assign bullet prefab in the Inspector
    public Transform startingPoint; // Starting point for the bullets

    [Header("Bullets")]
    public int TotalBullet = 50;
    public float bulletLifetime = 2f; // Time in seconds before the bullet is destroyed
    public float shootDelay = 0.5f; // Delay between each shot
    public float reloadTime = 2f; // Time it takes to reload after shooting all bullets

    private bool canShoot = true; // Flag to control shooting
    private int bulletsLeft; // Number of bullets left before reloading
    

    private void Start()
    {
        bulletsLeft = TotalBullet;
    }

    void Update()
    {
        if (!base.IsOwner) return;

        if (Input.GetKey(KeyCode.Space) && canShoot && bulletsLeft > 0)
        {
            StartCoroutine(ShootBulletWithDelay());
        }
    }

    public IEnumerator ShootBulletWithDelay()
    {
        canShoot = false; // Prevent shooting during delay
        Vector2 direction = startingPoint.right;
        ServerShootBullet(direction);
        bulletsLeft--;

        yield return new WaitForSeconds(shootDelay);

        canShoot = true; // Allow shooting again after delay

        // Check if reloading is needed
        if (bulletsLeft <= 0)
        {
            yield return new WaitForSeconds(reloadTime);
            bulletsLeft = TotalBullet; // Reset bullet count after reloading
        }
    }

    [ServerRpc]
    public void ServerShootBullet(Vector2 direction)
    {
        // Instantiate a bullet
        GameObject bullet = Instantiate(bulletPrefab, startingPoint.position, startingPoint.rotation);

        // Initialize the bullet with the direction
        bullet.GetComponent<Bullet>().Initialize(direction);

        // Set the bullet to be destroyed after a certain amount of time
        Destroy(bullet, bulletLifetime);

        // Spawn the bullet across the network
        base.Spawn(bullet);
    }
}

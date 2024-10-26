using System.Collections;
using UnityEngine;
using FishNet.Object;

public class JyGunRotation : NetworkBehaviour 
{
    public GameObject bulletPrefab; // Assign bullet prefab in the Inspector
    public Transform startingPoint; // Starting point for the bullets

    [Header("AimDirection")]
    public Transform Aim;
    public float aimDistance = 2f;
    private Joystick gunJoystick; // Reference to the Joystick component

    [Header("Bullets")]
    public int totalBullet = 50;
    public float bulletLifetime = 2f; // Time in seconds before the bullet is destroyed
    public float shootDelay = 0.5f; // Delay between each shot
    public float reloadTime = 2f; // Time it takes to reload after shooting all bullets

    private bool canShoot = true; // Flag to control shooting
    private int bulletsLeft; // Number of bullets left before reloading

    private void Awake()
    {
        // Find and assign the gun joystick reference
        gunJoystick = GameObject.Find("GunControll").GetComponent<Joystick>();
        if (gunJoystick == null)
        {
            Debug.LogError("Gun joystick not found! Make sure its name is 'GunControll' in the scene.");
        }

        bulletsLeft = totalBullet;
    }

    private void Update()
    {
        if (!base.IsOwner || gunJoystick == null) return;

        AimPosition();

        // Check if joystick input is significant enough to trigger shooting
        if (Mathf.Abs(gunJoystick.Horizontal) > 0.1f || Mathf.Abs(gunJoystick.Vertical) > 0.1f)
        {
            // If enough delay has passed and bullets are available, shoot
            if (canShoot && bulletsLeft > 0)
            {
                StartCoroutine(ShootBulletWithDelay());
            }
        }
    }


    private void AimPosition()
    {
        // Get input from the gun joystick
        float horizontal = gunJoystick.Horizontal;
        float vertical = gunJoystick.Vertical;

        // Calculate the aim direction based on joystick input
        Vector3 aimDirection = new Vector3(-horizontal, vertical, 0f).normalized * aimDistance;

        // Set the aim Transform position
        Aim.position = transform.position + aimDirection;

        // Calculate the angle between the gun's pivot and the aim position
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Rotate the gun sprite to face the aim direction around its pivot point
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

 private IEnumerator ShootBulletWithDelay()
{
    canShoot = false; // Prevent shooting during delay

    // Calculate the shooting direction based on joystick input
    Vector2 direction = startingPoint.right;

    ServerShootBullet(direction); // Shoot bullet towards the calculated direction
    bulletsLeft--; // Reduce the number of bullets left

    yield return new WaitForSeconds(shootDelay); // Wait for the shoot delay

    canShoot = true; // Allow shooting again after delay

    // Check if reloading is needed
    if (bulletsLeft <= 0)
    {
        yield return new WaitForSeconds(reloadTime); // Wait for the reload time
        bulletsLeft = totalBullet; // Reset bullet count after reloading
    }
}


    [ServerRpc]
    private void ServerShootBullet(Vector2 direction)
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

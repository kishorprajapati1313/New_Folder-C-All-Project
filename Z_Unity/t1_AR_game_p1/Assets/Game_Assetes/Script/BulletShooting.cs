using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 100f;
    public int bulletdamage = 10;

    void Update()
    {
        // Check for user input (mouse click or touch)
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Shoot();
        }
    }

   void Shoot()
{
    // Check if Camera.main is not null
    if (Camera.main != null)
    {
        // Instantiate a bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Get the bullet's rigidbody
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

        // Make the bullet face the same direction as the camera
        bullet.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

        // Add force to the bullet in the direction the gun is facing
        bulletRB.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);

        // Destroy the bullet after a certain time (adjust as needed)
        Destroy(bullet, 2f);
    }
    else
    {
        Debug.LogError("Main camera not found. Ensure your AR camera is tagged as 'MainCamera'.");
    }
}


    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")){
            EnemyHealth enemyhealth = other.GetComponent<EnemyHealth>();

            if(enemyhealth != null){
                enemyhealth.takeDamege(bulletdamage);
            }
        }    
        Destroy(gameObject);
    }

}

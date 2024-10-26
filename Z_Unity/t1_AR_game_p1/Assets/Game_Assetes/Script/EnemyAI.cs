using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float wanderRadius = 30f;
    public float wanderSpeed = 10f;
    public float chaseSpeed = 10f;
    public float attackDistance = 5f;
    public int attacksRemaining = 2;
    public int randomPointsCount = 0;
    public int maxRandomPoints = 3;
    public int attactDamage = 10;

    private Transform player;
    private Vector3 wanderTarget;
    private Animator Dragonanm;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Dragonanm = GetComponent<Animator>();
        StartCoroutine(EnemyBehavior());
    }

    IEnumerator EnemyBehavior()
    {
        while (true)
        {
            Dragonanm.Play("Fly Forward");
            // Check if it's time to chase the player
            if (randomPointsCount >= maxRandomPoints)
            {
                yield return StartCoroutine(ChasePlayer());
                randomPointsCount = 0; // Reset the counter after chasing
            }
            else
            {
                // Wander around randomly
                yield return StartCoroutine(Wander());
                randomPointsCount++;
            }
        }
    }

    IEnumerator Wander()
    {
        Dragonanm.Play("Fly Forward");
        // Get a random point within the wanderRadius
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection.y = 0.5f; // Keep the movement in the horizontal plane
        wanderTarget = transform.position + randomDirection;

        // Rotate towards the wanderTarget
        transform.LookAt(wanderTarget);

        // Move towards the wanderTarget
        while (Vector3.Distance(transform.position, wanderTarget) > 1f)
        {
            transform.Translate(Vector3.forward * wanderSpeed * Time.deltaTime);
            Dragonanm.Play("Fly Forward");
            yield return null;
        }

        // Pause before picking a new target
        yield return new WaitForSeconds(Random.Range(0.1f, 1f));
    }

    IEnumerator ChasePlayer()
    {
        while (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
                Dragonanm.Play("Fly Forward");

            // Calculate the direction towards the player, including height
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f; // Reset the y-component  

            // Rotate towards the player
            transform.rotation = Quaternion.LookRotation(directionToPlayer);

            // Move towards the player
            transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);

            yield return null;
        }

        // Attack the player
        for (int i = 0; i < attacksRemaining; i++)
        {
            // Implement your attack logic here
            // For example, play an animation or deal damage to the player
            Debug.Log("Dragon attacks!");
                Dragonanm.Play("Claw Attack");
            yield return new WaitForSeconds(3.2f); // Adjust this delay as needed
        }

        // Go to a random point before returning to wandering
        yield return StartCoroutine(ReturnToRandomPoint());
    }

    IEnumerator ReturnToRandomPoint()
    {
        Dragonanm.Play("Fly Forward");
        Vector3 returnPoint = transform.position + Random.insideUnitSphere * wanderRadius;
        returnPoint.y = 0f;

        while (Vector3.Distance(transform.position, returnPoint) > 1f)
        {
            // Rotate towards the returnPoint
            transform.LookAt(returnPoint);

            Dragonanm.Play("Fly Forward");
            // Move towards the returnPoint
            transform.Translate(Vector3.forward * wanderSpeed * Time.deltaTime);

            yield return null;
        }
    }

   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
             Player_Health playerhealth = gameObject.GetComponent<Player_Health>();
            if(playerhealth != null){
                Debug.Log("palyertaking the damage");
                playerhealth.takeDamege(attactDamage);
            }
        }
       
    }
}

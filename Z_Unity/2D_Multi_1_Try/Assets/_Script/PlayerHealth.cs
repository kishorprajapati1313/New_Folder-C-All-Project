using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar] // Synchronize health across the network
    public int health = 100;

    [SyncVar] // Synchronize kill count across the network
    public int killCount = 0;

    [SyncVar] // Synchronize death count across the network
    public int deathCount = 0;

    // Array of spawn points where the player can respawn
    public Transform[] spawnPoints;

    // Time delay before respawning
    public float respawnDelay = 1f;

    public void TakeDamage(int damage)
    {
        if (IsServer)
        {
            health -= damage;
            Debug.Log($"Player {Owner.ClientId} took damage. Health: {health}");

            if (health <= 0)
            {
                PlayerHealth killerHealth = GetKillerHealth();
                if (killerHealth != null)
                {
                    killerHealth.IncrementKillCount();
                }
                IncrementDeathCount();
                Die();
            }
        }
    }

    private void Die()
    {
        health = 100;
        Debug.Log($"Player {Owner.ClientId} died.");

        // Respawn the player after a delay
        StartCoroutine(RespawnAfterDelay(respawnDelay));
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Get a random spawn point
        Transform spawnPoint = GetRandomSpawnPoint();

        // Move the player to the spawn point
        transform.position = spawnPoint.position;

        // Reset health
        health = 100;
    }

    private Transform GetRandomSpawnPoint()
    {
        // Choose a random spawn point from the array
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }

    private PlayerHealth GetKillerHealth()
    {
        // Example: Find the killer's PlayerHealth component
        return null; 
    }

    private void IncrementKillCount()
    {
        killCount++;
        Debug.Log($"Player {Owner.ClientId} got a kill. Kill count: {killCount}");
    }

    private void IncrementDeathCount()
    {
        deathCount++;
        Debug.Log($"Player {Owner.ClientId} died. Death count: {deathCount}");
    }
}

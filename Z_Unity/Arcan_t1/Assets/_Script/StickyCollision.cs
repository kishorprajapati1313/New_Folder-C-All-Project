using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StickyCollision : MonoBehaviour
{
    public GameObject targetPrefab;
    public GameObject defeatPanel; // Reference to the defeat panel GameObject
    public TMP_Text scoreText; // Reference to the score text component
    public TMP_Text highestScoreText; // Reference to the highest score text component 

    public float minSpawnRadius = 2f; // Minimum distance for spawn
    public float maxSpawnRadius = 4f; // Maximum distance for spawn
    public float slowdownDuration = 0.2f; // Duration of slowdown in seconds

    private bool shouldSpawn = false;
    private bool hasDefeated = false;

    private int score = 0;
    private int highestScore = 0; // Variable to store the highest score

    [Header("InGame")]
    public TMP_Text scoreText2; // Reference to the score text component
    public TMP_Text highestScoreText2; // Reference to the highest score text component

    void Start()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore", 0); // Load the highest score from PlayerPrefs
        highestScoreText.text =  highestScore.ToString();
        highestScoreText2.text =  highestScore.ToString();
        SpawnObject();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            shouldSpawn = true;
        }
        scoreText2.text = score.ToString();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (shouldSpawn && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision Detected" + score);
            Destroy(collision.gameObject);
            score += 1;
            UpdateScore();
            SpawnObject();
            shouldSpawn = false;
            StartCoroutine(SlowDownTime());
        }
        else
        {
            hasDefeated = true;
            UpdateHighestScore(); // Check if the current score beats the highest score
        }
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    IEnumerator SlowDownTime()
    {
        Time.timeScale = 0.5f; // Slow down time
        yield return new WaitForSeconds(slowdownDuration); // Wait for the specified duration
        Time.timeScale = 1f; // Reset time scale to normal
    }

    private void SpawnObject()
    {
        float spawnDistance = Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPosition = spawnDirection * spawnDistance;

        GameObject newObject = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);

        // Get a random color excluding black
        Color randomColor = RandomColorExcludingBlack();
        // Apply the color to the object
        newObject.GetComponent<Renderer>().material.color = randomColor;
    }

    private Color RandomColorExcludingBlack()
    {
        Color randomColor;
        do
        {
            randomColor = new Color(Random.value, Random.value, Random.value);
        } while (randomColor == Color.black); // Repeat until a color that is not black is generated
        return randomColor;
    }

    void UpdateHighestScore()
    {
        if (score > highestScore)
        {
            highestScore = score;
            PlayerPrefs.SetInt("HighestScore", highestScore); // Save the new highest score to PlayerPrefs
            highestScoreText.text = highestScore.ToString();
        }
    }

    private void LateUpdate()
    {
        if (hasDefeated)
        {
            // Activate the defeat panel
            defeatPanel.SetActive(true);
        }
        else
        {
            // Deactivate the defeat panel
            defeatPanel.SetActive(false);
        }
    }
}

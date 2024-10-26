using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Complete_level : MonoBehaviour
{
    [SerializeField] private GameObject balloonParent; // Serialize the GameObject
    public bool levelComplete = false; // Make levelComplete static
    AudioManger audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManger>();
    }

    public void Update()
    {
        CheckLevelCompletion();
    }

    // Call this method whenever you need to check for level completion
    public void CheckLevelCompletion()
    {
        if (balloonParent == null)
        {
            Debug.LogWarning("Balloon parent is not assigned in the Inspector.");
            return;
        }

        int childCount = balloonParent.transform.childCount; // Get the child count

        if (childCount == 0 && !levelComplete) // Check if the level is not already marked as complete
        {
            levelComplete = true;
            audioManager.Playsfx(audioManager.complete);
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        // Wait for a short delay to ensure the completion message is visible
        yield return new WaitForSeconds(1f);

        // Pass level information and load the next scene
        Pass();

        // Load the scene after passing the level
        SceneManager.LoadScene("LevelSelecotor");
    }

    public void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        // Get the current highest completed level from PlayerPrefs
        if (currentLevel >= PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt("level", currentLevel + 1);
            Debug.Log("Level-" + PlayerPrefs.GetInt("level"));
        }
    }
}

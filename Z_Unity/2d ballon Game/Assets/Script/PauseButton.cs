using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenuUI;
     AudioManger audiomanager;

    private void Awake() {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManger>();
    }

    void Start()
    {
        
        // Ensure the pause menu UI is not active when the game starts
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void TogglePause()
    {
        audiomanager.Playsfx(audiomanager.click);

        // Toggle the pause state
        if (Time.timeScale == 0f)
        {
            // If the game is already paused, resume it
            ResumeGame();
        }
        else
        {
            // If the game is not paused, pause it
            PauseGame();
        }
    }

    void PauseGame()
    {
        audiomanager.Playsfx(audiomanager.click);
        // Pause the game
        Time.timeScale = 0f;
        // Show the pause menu UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
    }

    void ResumeGame()
    {
        audiomanager.Playsfx(audiomanager.click);

        // Resume the game
        Time.timeScale = 1f;
        // Hide the pause menu UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Debug.Log("Complete");
        }
    }

    public void ClosePauseMenu()
    {
        audiomanager.Playsfx(audiomanager.click);

        // If the close button in the pause menu is clicked, resume the game
        ResumeGame();
    }

    public void Restart()
    {
        audiomanager.Playsfx(audiomanager.click);

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
    }

    public void Menu(){
        audiomanager.Playsfx(audiomanager.click);

        SceneManager.LoadScene("LevelSelecotor");
    }
}

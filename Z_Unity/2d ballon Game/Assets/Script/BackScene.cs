using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour
{
    AudioManger audiomanager;

    private void Awake() {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManger>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BacktoLevelSelector(){
        audiomanager.Playsfx(audiomanager.click);
        SceneManager.LoadScene("LevelSelecotor");
    }

     public void BacktoMainMenu(){
        audiomanager.Playsfx(audiomanager.click);
        SceneManager.LoadScene("MainMeu");
    }

    public void Play(){
        audiomanager.Playsfx(audiomanager.click);
        SceneManager.LoadScene("LevelSelecotor");
    }

    public void Exit(){
        audiomanager.Playsfx(audiomanager.click);
        Application.Quit();
    }
    
}

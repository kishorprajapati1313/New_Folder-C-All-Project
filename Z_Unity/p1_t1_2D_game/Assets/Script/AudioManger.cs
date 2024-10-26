using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    [Header("-----------------Audio Source-----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("-----------------Audio Clip-----------------")]
    public AudioClip background;
    public AudioClip pop;
    public AudioClip clash;
    public AudioClip click;
    public AudioClip complete;

    private void Start(){
        musicSource.clip = background;
        musicSource.Play();
    }

    public void Playsfx(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }

    public static AudioManger instance;

    private void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance == null)
        {
            // If not, set this instance as the AudioManager
            instance = this;
            // Make this AudioManager undestroyable when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this AudioManager
            Destroy(gameObject);
        }
    }

    // public void SetSFXProperties(float speedMultiplier, float volume)
    // {
    //     // Adjust the pitch of the SFX source based on the speed multiplier
    //     sfxSource.pitch = speedMultiplier;

    //     // Set the volume of the SFX source
    //     sfxSource.volume = volume;
    // }

}

using UnityEngine;

public class Colision  : MonoBehaviour
{
    public Animator anim;
    public string balloonColor;
    AudioManger audiomanager;

    private void Awake() {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManger>();
    }

    private void Start() {
        anim = GetComponent<Animator>();    
         // Set the color of the balloon based on its tag
        if (gameObject.CompareTag("blueBallon"))
        {
            balloonColor = "blueBall";
        }
        else if (gameObject.CompareTag("redBallon"))
        {
            balloonColor = "redBallon";
        }
        else if (gameObject.CompareTag("grennBallon"))
        {
            balloonColor = "greenBallon";
        }else if (gameObject.CompareTag("orangeBallon"))
        {
            balloonColor = "OrangeBallon";
        }else if (gameObject.CompareTag("YellowBallon"))
        {
            balloonColor = "yellowBallon";
        }else if (gameObject.CompareTag("purpleBallon"))
        {
            balloonColor = "purpleBallon";
        }else if (gameObject.CompareTag("pinkBallon"))
        {
            balloonColor = "pinkBallon";
        }
    }
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //          anim.SetTrigger(balloonColor);
    //         Debug.Log("Player collided!");
    //         Destroy(gameObject, 2f);
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            audiomanager.Playsfx(audiomanager.pop);
            anim.SetTrigger(balloonColor);
            Debug.Log("Player collided!12");
            Destroy(gameObject, 1f);
        }
    }
}

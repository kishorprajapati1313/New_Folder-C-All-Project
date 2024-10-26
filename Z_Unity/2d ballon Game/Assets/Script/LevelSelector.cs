using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static int selectedLevel;
    public int level;
    public TextMeshProUGUI levelText;

     AudioManger audiomanager;

    private void Awake() {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManger>();
    }

    void Start()
    {
        levelText.text = level.ToString();
    }

    public void OpenScene()
    {
        audiomanager.Playsfx(audiomanager.click);
        selectedLevel = level;
        SceneManager.LoadScene("level-" + level.ToString());
    }
}

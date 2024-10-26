using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int levelcomplete;
    public Button[] bttons; // Assign in the Unity Editor

    void Start()
    {
        levelcomplete = PlayerPrefs.GetInt("levelcomplete", 1);

        // Iterate over the bttons array up to its length or the minimum of its length and the value of levelcomplete
        for (int i = 0; i < bttons.Length && i < levelcomplete; i++)
        {
            // Set the button interactable state to true
            bttons[i].interactable = true;

            // Find the "Correact" child object
            Transform correctChild = bttons[i].transform.Find("Correact");

            // Check if the correctChild is not null before accessing its gameObject
            if (correctChild != null)
            {
                // Activate the "Correact" child object
                correctChild.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Correact child object not found for button " + i);
            }
        }

        // // Disable any remaining buttons beyond the value of levelcomplete
        // for (int i = levelcomplete; i < bttons.Length; i++)
        // {
        //     bttons[i].interactable = false;
        // }
    }


}

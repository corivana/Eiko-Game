using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public static bool gameMenuOpen = false;
    public GameObject gameMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(gameMenuOpen) //if game menu is open, then close it, set everything back to normal
            {
                gameMenuUI.SetActive(false);
                Time.timeScale = 1f;
                gameMenuOpen = false;
                CameraController.instance.enabled = true;
                Cursor.lockState = CursorLockMode.Locked; //Hide cursor
            }
            else //open in game menu
            {
                gameMenuUI.SetActive(true);
                Time.timeScale = 0f; //stop time
                gameMenuOpen = true;
                CameraController.instance.enabled = false; //disable camera controller script bc I think it was causing issues when clicking on buttons
                Cursor.lockState = CursorLockMode.None; //Show cursor
            }
        }
    }

    public void SaveGame()
    {
        //TODO: Save the game
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}

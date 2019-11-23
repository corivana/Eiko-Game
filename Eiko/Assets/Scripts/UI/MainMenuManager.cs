using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        //TODO: Delete any old saves and load game
        //Loads the next scene after the Start Menu scene, which is the Hub
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame()
    {
        //TODO: Load Save Game
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}

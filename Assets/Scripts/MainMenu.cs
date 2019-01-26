using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public UnityEngine.UI.Button button;
    
    // Starts the first scene
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Play again from the ground floor
    public void ResumeGame()
    {
        SceneManager.LoadScene(1);
    }

    // Back from level to the main menu
    public void GameMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Select first Button for Controller compatabillity
    private void OnEnable()
    {
        button.Select();
    }
}

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

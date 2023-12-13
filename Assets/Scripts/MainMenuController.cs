using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnSettingsButton() 
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
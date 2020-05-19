using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; //Is the game paused
    public GameObject pauseMenu; //The pause menu gameobject
    public GameObject mobileHud; //The pause menu gameobject
    public GameObject SettingsMenu; //Settings gameobject
    public Settings settings; //Settings script

    void Update()
    {
        //If input for escape key and showInv is false
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Toggle pause state
            TogglePause();
        }
    }
    public void TogglePause()
    {
        //If the game is paused
        if (isPaused)
        {
            Time.timeScale = 1; //Sets the time scale
            if (settings.MobileHud)
            {
                mobileHud.SetActive(true);
            }
            pauseMenu.SetActive(false); //Hides the pause menu gameobject
            //settings.Save(); //Saves the settings
            SettingsMenu.SetActive(false); //Hides the settings menu gameobject
            isPaused = false; //Changes the bool for being paused
        }
        else
        {
            Time.timeScale = 0; //Sets the time scale to 0
            pauseMenu.SetActive(true); //shows the menu
            mobileHud.SetActive(false);
            isPaused = true;//Changes the bool for being paused
        }
    }
}

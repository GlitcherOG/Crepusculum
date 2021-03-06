﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[System.Serializable]
public class Settings : MonoBehaviour
{
    public Settings Instance;
    public Dropdown resolutionDropdown; //Dropdown for the resolution
    public Dropdown quailtyDropdown; //Dropdown for the quailty
    public Slider volumeSlider; //Slider for the volume
    Resolution[] resolutions; //An array used to store the resolutions
    public AudioMixer audioMixer; //The Audio mixer for the volume
    public float volume; //Used to store the volume float
    public bool MobileHud = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Error in Settings");
        }
    }
    void Start()
    {
        //Get the resolutions the screen can be and set it into the array
        resolutions = Screen.resolutions;
        //Clear all options on the dropdown
        resolutionDropdown.ClearOptions();
        //New list for all the resolution options
        List<string> options = new List<string>();
        //Set the currentResolutionIndex to zero
        int currentResolutionIndex = 0;
        //For all resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            //Set the new string option to contain the resoulution width and heigh
            string option = resolutions[i].width + " x " + resolutions[i].height;
            //Add the option to the list
            options.Add(option);
            //if the resolution matches the current resoultion
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                //Set the currentResolutionIndex to i
                currentResolutionIndex = i;
            }
        }
        //Set the quailty dropdown value to be the quality level
        quailtyDropdown.value = QualitySettings.GetQualityLevel();
        //Add the options to resolution
        resolutionDropdown.AddOptions(options);
        //Set the dropdown value to the current resolutionIndex
        resolutionDropdown.value = currentResolutionIndex;
        //Refresh the shown value
        resolutionDropdown.RefreshShownValue();
        //If the save file for settings isnt null
        if (SettingsBinary.LoadSettingsData() != null)
        {
            //Load settings
            Load();
        }
        else
        {
            //Save settings
            Save();
        }
    }

    public void Load()
    {
        //New SettingsData for the data loaded from settings file
        SettingsData data = SettingsBinary.LoadSettingsData();
        //Set the audioMixer volume to the data soundLevel
        audioMixer.SetFloat("Volume", data.soundLevel);
        //Change the volume text to show the volume percentage
        //volumeText.text = "Master Volume: " + Mathf.Round((((80f + data.soundLevel) / 80) * 100)).ToString() + "%";
        //Set the resolution dropdown value to the Data resolutionIndex
        resolutionDropdown.value = data.resolutionIndex;
        //Set the quailty dropdown value to the data quailtyIndex
        quailtyDropdown.value = data.quailtyIndex;
        //Set the quality level using the quailtyIndex
        QualitySettings.SetQualityLevel(data.quailtyIndex);
        //Set the volumeSlider value using the data sound level
        volumeSlider.value = data.soundLevel;
        MobileHud = data.mobileHud;
    }

    public void Save()
    {
        //Save settings data
        SettingsBinary.SaveSettingData(this);
    }

    public void SetVolume(float soundLevel)
    {
        //Set the audio mixers volume
        audioMixer.SetFloat("Volume", soundLevel);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        //Change the fullscreen state using the bool isFullScreen
        Screen.fullScreen = isFullscreen;
    }

    public void SetTouchControls(bool isControls)
    {
        MobileHud = isControls;
    }

    public void SetQuailty(int index)
    {
        //Set the quailty settings using the index
        QualitySettings.SetQualityLevel(index);
    }

    public void SetResolution(int index)
    {
        //Change the resolution of the screen using the array and index
        Resolution resolution = resolutions[index];
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullScreenToggle;
    public Resolution[] resolutions;
    private bool isFullScreen;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.options.Clear();

        List<string> resolutionList = new List<string>();

        int currentResolutionIndex = 0;
        int counter = 0;
        foreach (Resolution item in resolutions)
        {
            string option = item.width + " x " + item.height;
            resolutionList.Add(option);

            if (item.height == Screen.currentResolution.height && item.width == Screen.currentResolution.width)
            {
                currentResolutionIndex = counter;
            }
            counter++;
        }
        resolutionDropdown.AddOptions(resolutionList);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        QualitySettings.SetQualityLevel(qualityDropdown.value);
        if(PlayerPrefs.GetInt("FullScreen") == 1)
        {
            isFullScreen = true;
        }
        else
        {
            isFullScreen = false;
        }
        SetFullScreen(isFullScreen);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Quality", qualityDropdown.value);
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", VolumeCalculator(volume));
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        this.isFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    float VolumeCalculator(float volume)
    {
        float newVolume = (volume * 100) - 80;
        if (newVolume > 0)
        {
            return 0;
        }
        else
        {
            return newVolume;
        }
    }
}

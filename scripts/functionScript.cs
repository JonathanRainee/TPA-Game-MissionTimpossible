using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class function : MonoBehaviour

{
    public AudioMixer mixer;

    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int curr = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                curr = i;
            }
        }

        resolutionDropdown.value = curr;
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    public void setResolution(int residx)
    {
        Resolution res = resolutions[residx];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void Setvol (float vol)
    {
        mixer.SetFloat("volume", vol);
    }

    public void Setqual (int qualityidx)
    {
        QualitySettings.SetQualityLevel(qualityidx);
    }

    public void Setfull (bool isFull)
    {
        Screen.fullScreen = isFull;
    }
}

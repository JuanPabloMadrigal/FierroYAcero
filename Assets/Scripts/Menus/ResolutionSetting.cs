using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ResolutionSetting : MonoBehaviour
{
    public Toggle toggle;

    public TMP_Dropdown resolutionsDropdown;
    Resolution[] resolutions;
    // Start is called before the first frame update
    void Start()
    {
        if(Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        CheckResolution();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }


    public void CheckResolution()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> options = new List<string>();
        int iResolution = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            Debug.Log(resolutions[i]);
            string option = resolutions[i].width +" x "+ resolutions[i].height;
            options.Add(option);

            if(Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                iResolution = i;
            }
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = iResolution;
        resolutionsDropdown.RefreshShownValue();

        resolutionsDropdown.value = PlayerPrefs.GetInt("resolutionNum", 0);
    }
    public void ChangeResolution(int iResolution)
    {
        PlayerPrefs.SetInt("resolutionNum", resolutionsDropdown.value);
        Resolution resolution = resolutions[iResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

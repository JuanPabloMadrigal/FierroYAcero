using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Slider volumeSlider;
    public GameObject Slider;
    public GameObject videoPlayer;
    public GameObject resume;
    public GameObject options;
    public GameObject Exit;
    public GameObject multButton;
    public GameObject leaveOptions;
    public GameObject multiplayerCode;
    private AudioSource[] audioSources;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
        Slider.SetActive(false);
        Debug.Log("Empieza script");
    }
    public ShowDialogue showDialogue;
    public CameraMovement cameraMovement;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Detecta ESC");
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            try
            {
                showDialogue.onPause = pauseMenu.activeSelf;
            }
            catch { }
            Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
        }
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        try
        {
            showDialogue.onPause = false;
        }
        catch { }
    }

    public void OnExitButton()
    {
        FileHandlerStory.Instance.WriteFile();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnConfigurationButton()
    {
        resume.SetActive(false);
        try
        {
            Exit.SetActive(false);
        }
        catch { }
        options.SetActive(false);
        Slider.SetActive(true);
        multButton.SetActive(false);
        multiplayerCode.SetActive(false);
    }

    public void OnMultiplayerButton()
    {
        resume.SetActive(false);
        try
        {
            Exit.SetActive(false);
        }
        catch { }
        options.SetActive(false);
        Slider.SetActive(false);
        multButton.SetActive(false);
        multiplayerCode.SetActive(true);
    }
    public void OnMultiplayerExit()
    {
        resume.SetActive(true);
        try
        {
            Exit.SetActive(true);
        }
        catch { }
        options.SetActive(true);
        Slider.SetActive(false);
        multButton.SetActive(true);
        multiplayerCode.SetActive(false);
    }
    public void OnLeaveConfigurations()
    {
        resume.SetActive(true);
        try
        {
            Exit.SetActive(true);
        }
        catch { }
        options.SetActive(true);
        multButton.SetActive(true);
        Slider.SetActive(false);
        multiplayerCode.SetActive(false);
    }

    public void LeaveNotSaving()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void HandleVolumeChange(float volume)
    {
        AudioListener.volume = volume;
    }
}

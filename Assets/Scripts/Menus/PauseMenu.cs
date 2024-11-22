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
    public GameObject leaveOptions;
    private AudioSource[] audioSources;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
        Slider.SetActive(false);
    }
    public ShowDialogue showDialogue;
    public CameraMovement cameraMovement;

    public void OnEscKey(InputAction.CallbackContext ctx)
    {
        bool escKeyPressed = ctx.started;

        if (escKeyPressed)
        {
            pauseMenu.SetActive(true);
            showDialogue.onPause = true;
            Time.timeScale = 0; //if we add animators to buttons or want an animated menu, set animator controller update mode to unscaled
        } 
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        showDialogue.onPause = false;
    }

    public void OnExitButton()
    {
        FileHandlerStory.Instance.WriteFile();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnConfigurationButton()
    {
        resume.SetActive(false);
        Exit.SetActive(false);
        options.SetActive(false);
        Slider.SetActive(true);
    }
    public void OnLeaveConfigurations()
    {
        resume.SetActive(true);
        Exit.SetActive(true);
        options.SetActive(true);
        Slider.SetActive(false);
    }

    private void HandleVolumeChange(float volume)
    {
        AudioListener.volume = volume;
    }
}

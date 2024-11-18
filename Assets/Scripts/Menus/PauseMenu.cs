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
    private AudioSource[] audioSources;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
    }

    public void OnEscKey(InputAction.CallbackContext ctx)
    {
        bool escKeyPressed = ctx.started;

        if (escKeyPressed)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        } 
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void OnExitButton()
    {
        FileHandlerStory.Instance.WriteFile();
        SceneManager.LoadScene("MainMenu");
    }

    private void HandleVolumeChange(float volume)
    {
        AudioListener.volume = volume;
    }
}

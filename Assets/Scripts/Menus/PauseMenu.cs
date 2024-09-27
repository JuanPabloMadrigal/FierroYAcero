using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void OnEscKey(InputAction.CallbackContext ctx)
    {
        bool escKeyPressed = ctx.started;

        if (escKeyPressed)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0; //if we add animators to buttons or want an animated menu, set animator controller update mode to unscaled
        } 
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

}

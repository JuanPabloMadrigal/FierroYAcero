using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavegation : MonoBehaviour
{

    public GameObject CharacterMenu;
    public GameObject MainMenu;

    public void GoToChooseCharacter()
    {
        CharacterMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}

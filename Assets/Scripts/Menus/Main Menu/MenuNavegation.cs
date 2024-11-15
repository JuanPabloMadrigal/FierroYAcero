using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavegation : MonoBehaviour
{
    public GameObject CharacterMenu;
    public GameObject MainMenu;
    public GameObject videoPlayer;
    public GameObject GameSelectionMenu;
    public TextMeshProUGUI ContinueText;

    [SerializeField] EventTrigger continueButton;

    public float fadeSpeed;
    public GameObject loadScreen;
    private RawImage loadScreenImage;
    public Animator sceneAnimator;


    void Start()
    {
        ContinueGameExists();
        sceneAnimator = loadScreen.GetComponent<Animator>();
        Time.timeScale = 1;
    }

    public void GoToChooseCharacter()
    {
        CharacterMenu.SetActive(true);
        MainMenu.SetActive(false);
        GameSelectionMenu.SetActive(false);
    }

    public void GoToChooseGame(string flowType)
    {
        CharacterMenu.SetActive(false);
        MainMenu.SetActive(false);
        GameSelectionMenu.SetActive(true);
        GameSelectionMenu.GetComponent<ChooseGame>().LoadContextView(flowType);
    }

    public void GoToMainMenu()
    {
        CharacterMenu.SetActive(false);
        MainMenu.SetActive(true);
        GameSelectionMenu.SetActive(false);
    }

    public void GoToGame()
    {
        loadScreenImage = loadScreen.GetComponent<RawImage>();
        loadScreenImage.color = new Color(0, 0, 0, 0);
        loadScreen.SetActive(true);
        videoPlayer.SetActive(false);
        sceneAnimator.Play("FadeOut", -1, 0);
        StartCoroutine("FadeOut");
    }


    public void ContinueGameExists()
    {

        string fullPath1 = string.Empty;
        string fullPath2 = string.Empty;
        string fullPath3 = string.Empty;

        if (!PathManager.Instance.isLocal)
        {
            fullPath1 = Application.persistentDataPath + "/storyGame1.txt";
            fullPath2 = Application.persistentDataPath + "/storyGame2.txt";
            fullPath3 = Application.persistentDataPath + "/storyGame3.txt";
        }
        else
        {
            fullPath1 = Application.dataPath + "/Scripts/Story Mode/JSONs/storyGame1.txt";
            fullPath2 = Application.dataPath + "/Scripts/Story Mode/JSONs/storyGame2.txt";
            fullPath3 = Application.dataPath + "/Scripts/Story Mode/JSONs/storyGame3.txt";
        }

        bool fileValidation;
        fileValidation = (File.Exists(fullPath1) || File.Exists(fullPath2) || File.Exists(fullPath3)) ? true : false;
        //Debug.Log($"File validation for continue: {fileValidation}");

        if (fileValidation){
            ContinueText.color = new Color(0, 0, 0, 0.7f);
            continueButton.enabled = true;
        
        }
        else {
            ContinueText.color = new Color(0, 0, 0, 0.2f);
            continueButton.enabled = false;
        }
        
    }

    public void buttonColorOnHover(TMP_Text text)
    {
        text.color = new Color(0, 0, 0, 1f);
    }

    public void buttonColorOutHover(TMP_Text text)
    {
        text.color = new Color(0, 0, 0, 0.7f);
    }

    public IEnumerator FadeOut()
    {
        Debug.Log("Entra al intento de cambio de escena.");
        yield return new WaitForSecondsRealtime(fadeSpeed);
        Debug.Log("Espera el tiempo.");
        SceneManager.LoadScene("StoryGame");
        Debug.Log("Piensa que ya cambió de escena.");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }


}

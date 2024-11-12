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
    public float fadeSpeed;
    public GameObject loadScreen;
    private RawImage loadScreenImage;
    public GameObject CharacterMenu;
    public GameObject MainMenu;
    public GameObject MenuPartida;
    public Scene SavedGame;
    public TextMeshProUGUI ContinueText;

    [SerializeField] EventTrigger continueButton;

    public bool isLocal;
    public Animator sceneAnimator;
    
    void Start()
    {
        loadScreenImage = loadScreen.GetComponent<RawImage>();
        ContinueGameExists();
        sceneAnimator = loadScreen.GetComponent<Animator>();
    }

    public void GoToChooseCharacter()
    {
        CharacterMenu.SetActive(true);
        MainMenu.SetActive(false);
        MenuPartida.SetActive(false);
    }

    public void GoToChoosePartida()
    {
        CharacterMenu.SetActive(false);
        MainMenu.SetActive(false);
        MenuPartida.SetActive(true);
    }

    public void Return()
    {
        CharacterMenu.SetActive(false);
        MainMenu.SetActive(true);
        MenuPartida.SetActive(false);
    }


    public void ContinueGameExists()
    {

        bool fileValidation;

        if (!isLocal)
        {
            fileValidation = File.Exists(Application.persistentDataPath + "/storyGame.txt");
        }
        else
        {
            fileValidation = File.Exists(Application.dataPath + "/Scripts/Story Mode/JSONs/storyGame.txt");
        }
        
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


    public void ContinueGame()
    {
        loadScreenImage.color = new Color(0, 0, 0, 0);
        loadScreen.SetActive(true);
        sceneAnimator.Play("FadeOut");
        StartCoroutine("FadeOut");
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeSpeed);
        SceneManager.LoadScene("StoryGame");

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

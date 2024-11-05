using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavegation : MonoBehaviour
{

    public GameObject CharacterMenu;
    public GameObject MainMenu;
    public Scene SavedGame;
    public TextMeshProUGUI ContinueText;
    public Button Continue;

    [SerializeField]private bool isLocal;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        ContinueGameExists();
    }

    public void GoToChooseCharacter()
    {
        CharacterMenu.SetActive(true);
        MainMenu.SetActive(false);
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
            // Habilitar bioton
        ContinueText.color = new Color(0, 0, 0, 255);
        Debug.Log(ContinueText.color);
        Continue.enabled = true;
        
        }
        else {
            // No habilitar
        ContinueText.color = new Color(0, 0, 0, 100);
        Debug.Log(ContinueText.color);
        Continue.enabled = false;
        }
        
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene("StoryGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}

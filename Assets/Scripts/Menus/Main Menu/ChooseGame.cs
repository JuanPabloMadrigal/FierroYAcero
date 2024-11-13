using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ChooseGame : MonoBehaviour
{
    public MenuNavegation menuNavegation;
    public string flowType;
    public Button btn1;
    public Button btn2;
    public Button btn3;

    // Establece los botones que son interactuables dependiendo de si se hizo clic a continuar o nueva partida
    public void LoadContextView(string gameFlowType)
    {

        // Resetea los valores de color en los botones de numero de partida.
        ResetView();

        // Revisa si se hizo clic en continuar para revisar la existencia de todos los files de todos los save slots.
        if (gameFlowType == "Continuar")
        {
            GameNumberFileValidation();
        }

        flowType = gameFlowType;
    }

    public void ChooseSaveFile(int fileNum)
    {

        if (fileNum == 1)
        {
            PathManager.Instance.saveFileToUse = "storyGame1.txt";
        }
        else if (fileNum == 2)
        {
            PathManager.Instance.saveFileToUse = "storyGame2.txt";
        }
        else if (fileNum == 3)
        {
            PathManager.Instance.saveFileToUse = "storyGame3.txt";
        }
        else
        {
            Debug.Log("ERROR: No se pudo seleccionar un archivo de guardado, intente de nuevo.");
            return;
        }

        if (flowType == "Continuar")
        {
            menuNavegation.GoToGame();
        }
        else
        {
            menuNavegation.GoToChooseCharacter();
        }

    }

    private void GameNumberFileValidation()
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

        if (!File.Exists(fullPath1))
        {
            btn1.enabled = false;
            Color btnColor = btn1.gameObject.GetComponent<Image>().color;
            btnColor.a = 0.4f;
            btn1.gameObject.GetComponent<Image>().color = btnColor;
            foreach (RawImage childImage in btn1.gameObject.GetComponentsInChildren<RawImage>())
            {
                Color componentColor = childImage.color;
                componentColor.a = 0.4f;
                childImage.color = componentColor;
            }
            foreach (TMP_Text text in btn1.gameObject.GetComponentsInChildren<TMP_Text>())
            {
                Color textColor = text.color;
                textColor.a = 0.6f;
                text.color = textColor;
            }
        }

        if (!File.Exists(fullPath2))
        {
            btn2.enabled = false;
            Color btnColor = btn2.gameObject.GetComponent<Image>().color;
            btnColor.a = 0.4f;
            btn2.gameObject.GetComponent<Image>().color = btnColor;
            foreach (RawImage childImage in btn2.gameObject.GetComponentsInChildren<RawImage>())
            {
                Color componentColor = childImage.color;
                componentColor.a = 0.4f;
                childImage.color = componentColor;
            }
            foreach (TMP_Text text in btn2.gameObject.GetComponentsInChildren<TMP_Text>())
            {
                Color textColor = text.color;
                textColor.a = 0.6f;
                text.color = textColor;
            }
        }

        if (!File.Exists(fullPath3))
        {
            btn3.enabled = false;
            Color btnColor = btn3.gameObject.GetComponent<Image>().color;
            btnColor.a = 0.4f;
            btn3.gameObject.GetComponent<Image>().color = btnColor;
            foreach (RawImage childImage in btn3.gameObject.GetComponentsInChildren<RawImage>())
            {
                Color componentColor = childImage.color;
                componentColor.a = 0.4f;
                childImage.color = componentColor;
            }
            foreach (TMP_Text text in btn3.gameObject.GetComponentsInChildren<TMP_Text>())
            {
                Color textColor = text.color;
                textColor.a = 0.6f;
                text.color = textColor;
            }
        }

    }

    private void ResetView()
    {
        btn1.enabled = true;
        Color btnColor1 = btn1.gameObject.GetComponent<Image>().color;
        btnColor1.a = 1f;
        btn1.gameObject.GetComponent<Image>().color = btnColor1;
        foreach (RawImage childImage in btn1.gameObject.GetComponentsInChildren<RawImage>())
        {
            Color componentColor1 = childImage.color;
            componentColor1.a = 1f;
            childImage.color = componentColor1;
        }
        foreach (TMP_Text text in btn1.gameObject.GetComponentsInChildren<TMP_Text>())
        {
            Color textColor1 = text.color;
            textColor1.a = 1f;
            text.color = textColor1;
        }

        btn2.enabled = true;
        Color btnColor2 = btn2.gameObject.GetComponent<Image>().color;
        btnColor2.a = 1f;
        btn2.gameObject.GetComponent<Image>().color = btnColor2;
        foreach (RawImage childImage in btn2.gameObject.GetComponentsInChildren<RawImage>())
        {
            Color componentColor2 = childImage.color;
            componentColor2.a = 1f;
            childImage.color = componentColor2;
        }
        foreach (TMP_Text text in btn2.gameObject.GetComponentsInChildren<TMP_Text>())
        {
            Color textColor2 = text.color;
            textColor2.a = 1f;
            text.color = textColor2;
        }

        btn3.enabled = true;
        Color btnColor3 = btn3.gameObject.GetComponent<Image>().color;
        btnColor3.a = 1f;
        btn3.gameObject.GetComponent<Image>().color = btnColor3;
        foreach (RawImage childImage in btn3.gameObject.GetComponentsInChildren<RawImage>())
        {
            Color componentColor3 = childImage.color;
            componentColor3.a = 1f;
            childImage.color = componentColor3;
        }
        foreach (TMP_Text text in btn3.gameObject.GetComponentsInChildren<TMP_Text>())
        {
            Color textColor3 = text.color;
            textColor3.a = 1f;
            text.color = textColor3;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineHandler : MonoBehaviour
{

    [SerializeField] private int currentEvent;
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject dialogueScreen;

    void Update()
    {

        // Revisa cuando enseñar nuevo diálogo

        if (currentEvent == FileHandlerStory.Instance.gameData.evento - 1 && !dialogueScreen.activeInHierarchy)
        {
            currentEvent++;
            StartCoroutine(gameObject.GetComponent<ShowDialogue>().ImprimirDialogo(FileHandlerStory.Instance.gameDialogues.Eventos[currentEvent].Dialogos));
        }

        // Revisa cuando marcar que un evento ha sido completado

        /*if (currentEvent == 0 && leHaceClickAlLaboratorio)
        {
            FileHandlerStory.Instance.gameData.evento = 1;
        }*/

        if (currentEvent == 1 && FileHandlerStory.Instance.gameData.buildingsList[2].unlocked)
        {
            FileHandlerStory.Instance.gameData.evento = 2;
        }

        if (currentEvent == 2 && FileHandlerStory.Instance.gameData.ironStorehouse.unlocked && FileHandlerStory.Instance.gameData.cokePlant.unlocked)
        {
            FileHandlerStory.Instance.gameData.evento = 3;
        }

        if (currentEvent == 3 && FileHandlerStory.Instance.gameData.iron >= 5 && FileHandlerStory.Instance.gameData.coque >= 5)
        {
            FileHandlerStory.Instance.gameData.evento = 4;
        }
    }

    public void CheckForStoryProgress()
    {
        if (FileHandlerStory.Instance.gameData.evento == 0 && FileHandlerStory.Instance.gameData.buildingsList[2].unlocked == true)
        {
            FileHandlerStory.Instance.gameData.evento++;
        }
    }

    public void StartStorylineHandler()
    {
        currentEvent = FileHandlerStory.Instance.gameData.evento;
        TryBeginningDialogue();
    }

    private void TryBeginningDialogue()
    {
        if (currentEvent == -1)
        {
            indicator.SetActive(true);
        }
    }

}

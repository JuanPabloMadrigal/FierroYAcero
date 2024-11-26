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

        if (currentEvent < 6)
        {

            if (FileHandlerStory.Instance.gameData.evento == 0 && FileHandlerStory.Instance.gameData.buildingsList[4].unlocked == true)
            {
                FileHandlerStory.Instance.gameData.evento++;
            }

            if (FileHandlerStory.Instance.gameData.evento == 1 && FileHandlerStory.Instance.gameData.buildingsList[3].workersNum >= 100 && FileHandlerStory.Instance.gameData.buildingsList[4].workersNum >= 100)
            {
                FileHandlerStory.Instance.gameData.evento++;
            }

            if (currentEvent == 2 && FileHandlerStory.Instance.gameData.ironStorehouse.unlocked && FileHandlerStory.Instance.gameData.cokePlant.unlocked)
            {
                FileHandlerStory.Instance.gameData.evento++;
            }

            if (currentEvent == 3 && TurnManager.Instance.ironToAdd >= 30 && TurnManager.Instance.coqueToAdd >= 15)
            {
                FileHandlerStory.Instance.gameData.evento++;
            }

            if (currentEvent == 4 && TurnManager.Instance.steelToAdd >= 1)
            {
                FileHandlerStory.Instance.gameData.evento++;
            }

            if (currentEvent == 5 && TurnManager.Instance.steelToSubtract >= 1)
            {
                FileHandlerStory.Instance.gameData.evento++;
            }

            // Revisa cuando enseñar nuevo diálogo

            if (currentEvent == FileHandlerStory.Instance.gameData.evento - 1 && !dialogueScreen.activeInHierarchy)
            {
                currentEvent++;
                StartCoroutine(gameObject.GetComponent<ShowDialogue>().ImprimirDialogo(FileHandlerStory.Instance.gameDialogues.Eventos[currentEvent].Dialogos));
            }

        }
    }

    public void CheckForStoryProgress()
    {
        /*if (FileHandlerStory.Instance.gameData.evento == 6 && FileHandlerStory.Instance.gameData.eventProgress.turnsPassed >= 3 && FileHandlerStory.Instance.gameData.eventProgress.steelBought >= 3 && FileHandlerStory.Instance.gameData.eventProgress.steelSold >= 2)
        {
            indicator.Active(true);
            FileHandlerStory.Instance.gameData.ResetEventProgress();
        }*/



        if (currentEvent == FileHandlerStory.Instance.gameData.evento - 1 && !dialogueScreen.activeInHierarchy)
        {
            currentEvent++;
            StartCoroutine(gameObject.GetComponent<ShowDialogue>().ImprimirDialogo(FileHandlerStory.Instance.gameDialogues.Eventos[currentEvent].Dialogos));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineHandler : MonoBehaviour
{

    [SerializeField] private int currentEvent;

    private void Start()
    {
        currentEvent = FileHandlerStory.Instance.gameData.evento;
        StartCoroutine(TryBeginningDialogue());
    }

    void Update()
    {
        if (currentEvent == FileHandlerStory.Instance.gameData.evento - 1)
        {
            currentEvent++;
            StartCoroutine(gameObject.GetComponent<ShowDialogue>().ImprimirDialogo(FileHandlerStory.Instance.gameDialogues.Eventos[currentEvent].Dialogos));
        }
    }

    public void CheckForStoryProgress()
    {
        if (FileHandlerStory.Instance.gameData.evento == 0 && FileHandlerStory.Instance.gameData.buildingsList[2].unlocked == true)
        {
            FileHandlerStory.Instance.gameData.evento++;
        }
    }

    private IEnumerator TryBeginningDialogue()
    {
        yield return new WaitForSeconds(0.1f);
        if (currentEvent == 0)
        {
            StartCoroutine(gameObject.GetComponent<ShowDialogue>().ImprimirDialogo(FileHandlerStory.Instance.gameDialogues.Eventos[currentEvent].Dialogos));
        }
    }

}

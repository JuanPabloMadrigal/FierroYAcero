using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.Video;


public class ShowDialogue : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject character;
    public GameObject dialogue;
    public GameObject info;
    public GameObject Bton;

    public GameObject VideoFilter;

    private GameObject TempVF;

    public GameObject MainCamera;

    private void Start()
    {
        Bton.SetActive(false);
        info.SetActive(false);
        dialogue.SetActive(false);
        character.SetActive(false);
    }
    /**
    private void OnPointerDown(PointerEventData eventData)
    {

    }

    private void OnPointerUP(PointerEventData eventData)
    {

    }
    **/
    public void OnPointerEnter(PointerEventData eventData)
    {

        info.SetActive(true);
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        info.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        info.SetActive(false);
        Bton.SetActive(true);
        character.SetActive(true);
        dialogue.SetActive(true);
        TempVF = Instantiate(VideoFilter, new Vector3( 19, 19, 19), Quaternion.identity);
        TempVF.GetComponent<VideoPlayer>().targetCamera = MainCamera.GetComponent<Camera>();
    }

    public void OnClick()
    {
        Bton.SetActive(false);
        dialogue.SetActive(false);
        character.SetActive(false);
        Destroy(TempVF);

    }
        // Update is called once per frame

    }

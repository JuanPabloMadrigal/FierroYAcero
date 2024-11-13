using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.Video;


public class ShowDialogue : MonoBehaviour, IPointerClickHandler
{
    public GameObject img_izq;
    public GameObject img_der;

    // Sprites de dialogos
    [SerializeField] private Sprite edFeliz;
    [SerializeField] private Sprite edNormal;
    [SerializeField] private Sprite edEstresado;
    [SerializeField] private Sprite carFeliz;
    [SerializeField] private Sprite carNormal;
    [SerializeField] private Sprite carEstresado;
    [SerializeField] private Sprite viFeliz;
    [SerializeField] private Sprite viNormal;
    [SerializeField] private Sprite viTriste;
    [SerializeField] private Sprite viEnojado;


    // Objetos visuales de la escena
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject name;

    public GameObject Bton; 
    public GameObject VideoFilter;

    private GameObject TempVF;

    public GameObject MainCamera;

    /*public void OnPointerEnter(PointerEventData eventData)
    {

        info.SetActive(true);
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        info.SetActive(false);
    }*/
    public void OnPointerClick(PointerEventData eventData)
    {
        TempVF = Instantiate(VideoFilter, new Vector3( 19, 19, 19), Quaternion.identity);
        TempVF.GetComponent<VideoPlayer>().targetCamera = MainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    public IEnumerator ImprimirDialogo(List<Dialogo> evento)
    {

        dialogueCanvas.SetActive(true);

        int dialogueNumber = 0;

        while (dialogueNumber < evento.Count)
        {
            if (dialogue.GetComponent<TextMeshProUGUI>().text != evento[dialogueNumber].dialogoTexto)
            {

                if (evento[dialogueNumber].img_izq == "Jugador_Normal" && FileHandlerStory.Instance.gameData.character.ToLower() == "eduardo")
                {
                    img_izq.GetComponent<Image>().sprite = edNormal;
                }
                else if (evento[dialogueNumber].img_izq == "Jugador_Normal" && FileHandlerStory.Instance.gameData.character.ToLower() == "carmina")
                {
                    img_izq.GetComponent<Image>().sprite = carNormal;
                }
                else if (evento[dialogueNumber].img_izq == "Jugador_Feliz" && FileHandlerStory.Instance.gameData.character.ToLower() == "eduardo")
                {
                    img_izq.GetComponent<Image>().sprite = edFeliz;
                }
                else if (evento[dialogueNumber].img_izq == "Jugador_Feliz" && FileHandlerStory.Instance.gameData.character.ToLower() == "carmina")
                {
                    img_izq.GetComponent<Image>().sprite = carFeliz;
                }
                else if (evento[dialogueNumber].img_izq == "Jugador_Estresado" && FileHandlerStory.Instance.gameData.character.ToLower() == "eduardo")
                {
                    img_izq.GetComponent<Image>().sprite = edEstresado;
                }
                else if (evento[dialogueNumber].img_izq == "Jugador_Estresado" && FileHandlerStory.Instance.gameData.character.ToLower() == "carmina")
                {
                    img_izq.GetComponent<Image>().sprite = carEstresado;
                }


                if (evento[dialogueNumber].img_der == "Vicente_Normal")
                {
                    img_der.GetComponent<Image>().sprite = viNormal;
                }
                else if (evento[dialogueNumber].img_der == "Vicente_Feliz")
                {
                    img_der.GetComponent<Image>().sprite = viFeliz;
                }
                else if (evento[dialogueNumber].img_der == "Vicente_Triste")
                {
                    img_der.GetComponent<Image>().sprite = viTriste;
                }
                else if (evento[dialogueNumber].img_der == "Vicente_Enojado")
                {
                    img_der.GetComponent<Image>().sprite = viEnojado;
                }

                string newDialogue = evento[dialogueNumber].dialogoTexto.Replace("{nombreJugador}", FileHandlerStory.Instance.gameData.character);
                string newName = evento[dialogueNumber].nombre.Replace("{nombreJugador}", FileHandlerStory.Instance.gameData.character);

                dialogue.GetComponent<TextMeshProUGUI>().text = newDialogue;
                name.GetComponent<TextMeshProUGUI>().text = newName;

            }


            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            dialogueNumber++;

        }

        dialogueCanvas.SetActive(false);

    }

 }

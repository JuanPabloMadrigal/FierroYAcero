using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddProfit : MonoBehaviour
{
    public void AddTest()
    {
        FileHandlerStory.Instance.gameData.money += 100;
        foreach (BuildingProperties buildingData in FileHandlerStory.Instance.gameData.buildingsList)
        {
            buildingData.AddModifier(0.2f);
        }
        UIManager.Instance.UpdateMoneyUI(FileHandlerStory.Instance.gameData.money);
    }

    public void SaveGame()
    {
        FileHandlerStory.Instance.WriteFile();
    }

    public void LoadGame()
    {
        FileHandlerStory.Instance.ReadFile();
    }
    public void EncryptFile()
    {
        FileHandlerStory.Instance.EncryptExternalFile("{\"Eventos\":[{\"Dialogos\":[{\"img_der\":\"Vicente_Feliz\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"�Bienvenido a Fundidora Fierro y Acero! Soy Vicente Ferrara, el anterior director, y estar� gui�ndote en este emocionante viaje. �Comencemos con lo b�sico!\"},{\"img_der\":\"Vicente_Normal\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"Primero, observa a tu alrededor. Aqu� tienes la planta de Fundidora Fierro y Acero, un lugar donde el trabajo duro y la innovaci�n se unen para crear acero de calidad.\"},{\"img_der\":\"Vicente_Feliz\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"Una vez que hayas aprendido a gestionar tus recursos, podr�s invertir en mejoras.\"},{\"img_der\":\"Vicente_Normal\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"Ahora observa que en la parte de abajo tienes tu men� de construcci�n.\"}]},{\"Dialogos\":[{\"img_der\":\"Vicente_Normal\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"Aqu� podr�s encontrar los diferentes edificios que tengas a tu disposici�n.\"}]},{\"Dialogos\":[{\"img_der\":\"Vicente_Normal\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"Como puedes observar, el edificio ha aparecido en el mapa.\"},{\"img_der\":\"Vicente_Feliz\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"�Este ser�, por ahora, tu edificio m�s importante!\"},{\"img_der\":\"Vicente_Normal\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"Recuerda, cada edificio tiene su propio prop�sito.\"},{\"img_der\":\"Vicente_Normal\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"Ahora que conoces el proceso, posiciona en el mapa el Almac�n de Materia Prima.\"}]},{\"Dialogos\":[{\"img_der\":\"Vicente_Normal\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"El Almac�n de Materia Prima y la Planta de Coque te permitir�n adquirir material.\"},{\"img_der\":\"Vicente_Normal\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"Recuerda que ciertos edificios te permitir�n transformar materia prima en productos terminados.\"},{\"img_der\":\"Vicente_Feliz\",\"img_izq\":\"Jugador_Normal\",\"DialogoTexto\":\"Con esto, ya est�s preparado para administrar la planta Fundidora como su nuevo director.\"}]}]}\r\n", "storyDialogues.txt");
    }

}

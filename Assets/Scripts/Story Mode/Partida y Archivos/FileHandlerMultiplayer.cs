using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FileHandlerMultiplayer : MonoBehaviour
{
    /// <summary>
    /// Manejo de clase como singleton
    /// </summary>
    public static FileHandlerMultiplayer Instance;

    // VARIABLE QUE GUARDA EL MODELO DE JUEGO DE LA PARTIDA

    public GameModel gameData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    

    void RefreshEditorProjectWindow()
    {
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif
    }

}

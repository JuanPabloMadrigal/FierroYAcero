using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using TMPro;

public class PathManager : MonoBehaviour
{

    public static PathManager Instance;
    public bool isLocal;
    public string saveFileToUse;
    //private int firstRun;
    public TMP_Text debug;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        SetFilesDirectory();

    }

    private void Start()
    {

        

        

        /*if (!isLocal)
        {
            firstRun = PlayerPrefs.GetInt("First Run", 0);
            debug.text = firstRun.ToString();

            if (firstRun == 0)
            {
                foreach (var file in Directory.GetFiles(Application.persistentDataPath))
                {
                    FileInfo file_info = new FileInfo(file);
                    if (file_info.Extension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        file_info.Delete();
                    }
                }

                PlayerPrefs.SetInt("First Run", 1);

            }
        }*/

        debug.text = isLocal.ToString();

    }

    public void SetFilesDirectory()
    {
        #if UNITY_EDITOR
                    isLocal = true;
        #else
                    isLocal = false;
        #endif
    }

}

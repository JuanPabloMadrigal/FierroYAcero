using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignStartingValues : MonoBehaviour
{
    private GameModel gameModel;

    void Start()
    {
        gameModel = GameObject.FindGameObjectWithTag("GameProp").GetComponent<GameModel>();
        gameModel.AddMoney(0);
    }
}

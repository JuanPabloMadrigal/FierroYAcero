using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CokePlant
{

    public int costPerTurn;
    public int costToBuild;
    public bool unlocked;
    public float x;
    public float y;
    public float z;
    public float rot;

    public CokePlant(int costPerTurn, int costToBuild, bool unlocked, float x, float y, float z, float rot)
    {
        this.costPerTurn = costPerTurn;
        this.costToBuild = costToBuild;
        this.unlocked = unlocked;
        this.x = x;
        this.y = y;
        this.z = z;
        this.rot = rot;
    }

}

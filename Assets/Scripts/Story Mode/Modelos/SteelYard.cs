using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelYard
{

    public int costPerTurn;
    public int costToBuild;
    public int pureIron;
    public int ironBars;
    public int rails;
    public bool unlocked;
    public float x;
    public float y;
    public float z;
    public float rot;

    public SteelYard(int costPerTurn, int costToBuild, bool unlocked, float x, float y, float z, float rot)
    {
        this.costPerTurn = costPerTurn;
        this.costToBuild = costToBuild;
        pureIron = 0;
        ironBars = 0;
        rails = 0;
        this.unlocked = unlocked;
        this.x = x;
        this.y = y;
        this.z = z;
        this.rot = rot;
    }

}

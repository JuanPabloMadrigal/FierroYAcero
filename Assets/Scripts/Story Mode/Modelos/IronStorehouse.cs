using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronStorehouse
{

    public int costPerTurn;
    public int costToBuild;
    public int ironCount;
    public bool unlocked;
    public string type;
    public float x;
    public float y;
    public float z;
    public float rot;

    public IronStorehouse(int costPerTurn, int costToBuild, bool unlocked, string type, float x, float y, float z, float rot)
    {
        this.costPerTurn = costPerTurn;
        this.costToBuild = costToBuild;
        ironCount = 0;
        this.unlocked = unlocked;
        this.type = type;
        this.x = x;
        this.y = y;
        this.z = z;
        this.rot = rot;
    }

}

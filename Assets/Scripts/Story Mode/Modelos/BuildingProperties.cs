using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingProperties
{
    public int costPerTurn;
    public int costToBuild;
    public float addingValue;
    public float valueModifier;
    public int workersNum;
    public bool unlocked;
    public string type;
    public float x;
    public float y;
    public float z;
    public float rot;


    public BuildingProperties(int turnCost, int buildCost, float addValue, float valMod, int workN, bool unl, string tp, float posx, float posy, float posz, float rt)
    {
        costPerTurn = turnCost;
        costToBuild = buildCost;
        addingValue = addValue;
        valueModifier = valMod;
        workersNum = workN;
        unlocked = unl;
        type = tp;
        x = posx; 
        y = posy; 
        z = posz;
        rot = rt;
    }

    public void AddModifier(float val)
    {
        valueModifier = RoundValues(valueModifier + val, 1);
    }

    public float RoundValues(float value, int decimals)
    {
        float multiplier = Mathf.Pow(10, decimals);
        return Mathf.Round(value * multiplier) / multiplier;
    }

}

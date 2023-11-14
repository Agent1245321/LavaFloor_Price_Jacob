using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool[] unlockedLevels;
    public float[] timers;

    public PlayerData(MenuScript manager) 
    {
        unlockedLevels = manager.levelData;  
        timers = manager.timers;
    }

}

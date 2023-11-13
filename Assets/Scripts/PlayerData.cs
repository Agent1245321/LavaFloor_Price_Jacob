using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool[] unlockedLevels;

    public PlayerData(MenuScript manager) 
    {
        unlockedLevels = manager.levelData;    
    }

}

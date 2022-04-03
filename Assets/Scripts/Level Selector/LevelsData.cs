using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelsData
{
    public int latestClearedLevel;

    public LevelsData(int clearedLevel)
    {
        latestClearedLevel = clearedLevel;
    }
}



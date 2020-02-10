using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //Either a hash table[HARD], int to bitArray for saving bools into bits(less data)[MEDIUM] or a fixed bool array[EASY] in which every bool is one byte(more data)
    public string playerName;
    public bool[,] levelDone = new bool[3,6];

    public SaveData(MainMenu TheSaveData)
    {
        for (int p = 0; p < 3; p++)
        {
            for (int z = 0; z < 6; z++)
            {
                levelDone[p, z] = MainMenu.levelComplete[p, z];
            }
        }
    }
}

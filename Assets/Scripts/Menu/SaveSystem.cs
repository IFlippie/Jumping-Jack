using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //the save and load methods both use the binary formatter to stream the variables from the savedata class into a data file
    //in both cases you call the formatter and path to use and then you serialize/deserialize the data from the savedata class and close the stream afterwards 
    public static void SaveTheData(MainMenu TheSaveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "saveFile.balls");
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(TheSaveData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadTheData()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveFile.balls");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("Balls found in " + path);
            return data;
        }
        else
        {
            Debug.Log("Balls not found in " + path);
            return null;
        }

    }



}

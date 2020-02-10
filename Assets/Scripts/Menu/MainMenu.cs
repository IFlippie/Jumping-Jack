using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainMenu : MonoBehaviour
{
    //private string sceneSelected;
    //private string findButton;
    //private Button playButton;
    //public static float startingTime;

    //Variables for Ingame Levels
    public static int loseLevel = 0;
    private int gameSceneNumber = 1;
    
    //2D array for keeping track of completed levels
    public static bool[,] levelComplete = new bool[3, 6];

    //Level 1 is set to true for first time players to make it accessible along with the save file loading
    void Start()
    {       
        levelComplete[0, 0] = true;
        string path = Path.Combine(Application.persistentDataPath, "saveFile.balls");
        if (File.Exists(path))
        {
            LoadFile();
        }
        else { Debug.Log("nothing loaded"); }
        for (int p = 0; p < 3; p++)
        {
            for (int z = 0; z < 6; z++)
            {
                //Debug.Log(levelComplete[p, z] + " " + p + " " + z);
            }
        }
    }

    //public void PlayLevel()
    //{
    //    findButton = EventSystem.current.currentSelectedGameObject.name;
    //    //playButton = GameObject.Find(findButton).GetComponent<Button>();
    //    Debug.Log(findButton[5]);
    //    int a = System.Convert.ToInt32(findButton[5]) - 48;
    //    Debug.Log(a);
    //    loseLevel = a;
    //    AnalyticsEvent.Custom("Game started at", new Dictionary<string, object>
    //    {
    //        { "TIME", Time.time }
    //    });
    //    startingTime = Time.time;
    //    //sceneSelected = playButton.GetComponentInChildren<Text>().text//.ToString();
    //    //Debug.Log(sceneSelected);
    //    SceneManager.LoadScene(a);
    //}

    //Methods for accessing the save and load features
    public void QuitButton()
    {
        Application.Quit();
    }
    public void SaveFile()
    {
        SaveSystem.SaveTheData(this);
    }
    public void LoadFile()
    {
        SaveData data = SaveSystem.LoadTheData();
        for (int p = 0; p < 3; p++)
        {
            for (int z = 0; z < 6; z++)
            {
                levelComplete[p, z] = data.levelDone[p, z];
            }
        }
    }
}


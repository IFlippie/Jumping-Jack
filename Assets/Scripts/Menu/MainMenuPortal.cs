using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class MainMenuPortal : MonoBehaviour
{
    //locking levels by checking the save file data if the previous level is completed or not
    [SerializeField] int nextSceneNumber;
    [SerializeField] int previousW;
    [SerializeField] int previousL;
    public bool collided;
    Color original;
    MainMenu mainmenu;

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            collided = true;
            if (MainMenu.levelComplete[previousW, previousL])
            {  //throw           
                SceneManager.LoadScene(nextSceneNumber);
                MainMenu.loseLevel = nextSceneNumber;
                //AnalyticsEvent.Custom("Level completed at", new Dictionary<string, object>
                // {
                //        { "Time", Time.time }
                // });
            }
            else {
                original = GetComponent<Renderer>().material.color;
                GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
    public void OnCollisionExit(Collision other)
    {
        GetComponent<Renderer>().material.color = original;
    }
    }

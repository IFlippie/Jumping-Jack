using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPortal : MonoBehaviour
{
    //locking levels by checking the save file data if the previous level is completed or not
    [SerializeField] int nextSceneNumber;
    [SerializeField] int previousW;
    [SerializeField] int previousL;
    Color original;
    MainMenu mainmenu;

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            if (MainMenu.levelComplete[previousW, previousL])
            {  //throw           
                SceneManager.LoadScene(nextSceneNumber);
                MainMenu.loseLevel = nextSceneNumber;             
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

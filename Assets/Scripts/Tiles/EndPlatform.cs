using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPlatform : MonoBehaviour
{
    [SerializeField] int nextSceneNumber;
    [SerializeField] int world;
    [SerializeField] int level;
    public GameObject scriptReference;

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            if (MainMenu.levelComplete[world, level])
            {
                Debug.Log("Already saved/completed");
            }
            else {
                MainMenu.levelComplete[world, level] = true;
                scriptReference.GetComponent<MainMenu>().SaveFile();
            }
            SceneManager.LoadScene(nextSceneNumber);
            MainMenu.loseLevel = nextSceneNumber;
        }
    }
}

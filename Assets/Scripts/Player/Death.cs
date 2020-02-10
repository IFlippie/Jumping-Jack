using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    //private int loseScreenNumber = 2;
    private int minY = -20;

    void Update()
    {
        if (transform.position.y < minY) {
            KillPlayer();
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            //KillPlayer();
        }
    }

    private void KillPlayer() {
        SceneManager.LoadScene(MainMenu.loseLevel);
    }
}

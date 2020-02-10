using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    private int mainMenuNumber = 0;
    private int playGameNumber = 1;

    // Update is called once per frame
    //public void MenuButton()
    //{
    //    SceneManager.LoadScene(mainMenuNumber);
    //}

    public void nextButton() {
        SceneManager.LoadScene(MainMenu.loseLevel);
        MainMenu.loseLevel += 1;
    }
}

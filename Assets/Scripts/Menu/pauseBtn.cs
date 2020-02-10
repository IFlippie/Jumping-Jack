using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseBtn : MonoBehaviour
{
    public bool paused = false;
    public Image backgroundImage;
    public GameObject backButton;
    public GameObject restartButton;

    void Start()
    {
        backgroundImage.canvasRenderer.SetAlpha(0f);
    }
    
    public void Pause()
    {
        if (paused)
        {
            backgroundImage.CrossFadeAlpha(0, 0.2f, true);
            backButton.SetActive(false);
            restartButton.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
        }
        else
        {
            backgroundImage.CrossFadeAlpha(1, 0.2f, true);
            backButton.SetActive(true);
            restartButton.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
        
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        MainMenu.loseLevel = 0;
        Time.timeScale = 1f;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(MainMenu.loseLevel);
        Time.timeScale = 1f;
    }

}

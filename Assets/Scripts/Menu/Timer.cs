using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public GameObject timerObject;
    public float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timerObject = GameObject.Find("TimerText");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        timerText.text = Math.Round(currentTime, 1).ToString();
        timerObject.GetComponent<Text>().text = Math.Round(currentTime, 1).ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public int minutesTimer = 15, secondsTimer;

    private DateTime timer = new DateTime();
    private int minutesLeft, secondsLeft;

    private bool run = true;

    private void Update()
    {
        timer = timer.AddSeconds(Time.deltaTime);
        if (run)
        {
            minutesLeft = minutesTimer - timer.Minute;
            secondsLeft = secondsTimer - timer.Second;
            if (minutesLeft == 0 && secondsLeft == 0)
            {
                Empty();
                run = false;
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Timer " + "Minutes " + minutesLeft + "Seconds " + secondsLeft);
    }

    private void Empty()
    {
        Debug.Log("Time left");
    }
}
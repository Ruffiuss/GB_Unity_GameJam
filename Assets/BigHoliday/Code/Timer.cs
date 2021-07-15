using UnityEngine;
using UnityEngine.UI;
using System;


public class Timer : MonoBehaviour
{
    public int minutesTimer = 15, secondsTimer;

    private DateTime timer = new DateTime();
    private Text _text;

    private int minutesLeft, secondsLeft;
    private bool run = true;

    private void Awake()
    {
        _text = gameObject.GetComponent<Text>();
    }

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
            _text.text = $"Time left:{secondsLeft}";
        }
    }

    private void Empty()
    {
        Time.timeScale = 0;
        _text.text = "Enought for today!";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private bool gameStopped = false;
    private int timer = 0; 
    public void StartClock()
    {
        gameStopped = false;
        StartCoroutine(StartTimer());
    }

    public void StopClock(bool win)
    {
        gameStopped = true;
        if (win) GameData.SubmitTime(timer);
    }

    public void ResetClock()
    {
        timer = 0;
        timerText.text = "0:00";
    }

    IEnumerator StartTimer()
    {
        var second = new WaitForSecondsRealtime(1);
        while (!gameStopped)
        {
            yield return second;
            if (!gameStopped)
            {
                timer++;
                TimeSpan currentTime = TimeSpan.FromSeconds(timer);
                timerText.text = currentTime.Seconds < 10 ? $"{currentTime.Minutes}:0{currentTime.Seconds}" : $"{currentTime.Minutes}:{currentTime.Seconds}";
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int rowCount = 9;
    public static int colCount = 9;
    public static int bombCount = 10;
    public static int highscore = int.MaxValue;

    public static void SubmitTime(int timer)
    {
        if(timer < highscore)
        {
            highscore = timer;
        }
        Debug.Log($"Highscore: {highscore}");
    }
}

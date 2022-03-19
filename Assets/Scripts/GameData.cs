using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static BoardCombination boardCombination = new BoardCombination(9, 9, 10);
    public static Dictionary<BoardCombination, int> highscores = new Dictionary<BoardCombination, int>();
    public static int rowCount { get => boardCombination.rowCount; set => boardCombination.rowCount = value; }
    public static int colCount { get => boardCombination.colCount; set => boardCombination.colCount = value; }
    public static int bombCount { get => boardCombination.bombCount; set => boardCombination.bombCount = value; }

    public static void SubmitTime(int timer)
    {
        if (highscores.ContainsKey(boardCombination))
        {
            if (timer < highscores[boardCombination])
            {
                highscores[boardCombination] = timer;
            }
            else
            {
                //have a 10 array of highscores
            }
        }
        else
        {
            highscores.Add(boardCombination, timer);
        }

        Debug.Log($"Highscore for row:{rowCount} col:{colCount} bomb:{bombCount} => {highscores[boardCombination]}");
    }

    public struct BoardCombination
    {
        public int rowCount;
        public int colCount;
        public int bombCount;

        public BoardCombination(int rowCount, int colCount, int bombCount)
        {
            this.rowCount = rowCount;
            this.colCount = colCount;
            this.bombCount = bombCount;
        }
    }
}

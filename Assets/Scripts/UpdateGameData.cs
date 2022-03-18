using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UpdateGameData : MonoBehaviour
{
    [SerializeField] private TMP_InputField rowCountText;
    [SerializeField] private TMP_InputField colCountText;
    [SerializeField] private TMP_InputField bombCountText;
    void Start()
    {
        rowCountText.text = GameData.rowCount.ToString();
        colCountText.text = GameData.colCount.ToString();
        bombCountText.text = GameData.bombCount.ToString();
    }

    public void Increase(string name)
    {
        switch (name)
        {
            case "Row":
                GameData.rowCount++;
                rowCountText.text = GameData.rowCount.ToString();
                break;
            case "Col":
                GameData.colCount++;
                colCountText.text = GameData.colCount.ToString();
                break;
            case "Bomb":
                GameData.bombCount++;
                bombCountText.text = GameData.bombCount.ToString();
                break;
            default:
                Debug.Log("probably fucked up a string Lamo");
                break;
        }
    }

    public void Decrease(string name)
    {
        switch (name)
        {
            case "Row":
                GameData.rowCount--;
                rowCountText.text = GameData.rowCount.ToString();
                break;
            case "Col":
                GameData.colCount--;
                colCountText.text = GameData.colCount.ToString();
                break;
            case "Bomb":
                GameData.bombCount--;
                bombCountText.text = GameData.bombCount.ToString();
                break;
            default:
                Debug.Log("probably fucked up a string Lamo");
                break;
        }
    }

    public void SetRowCount(string amount)
    {
        int number;
        GameData.rowCount = int.TryParse(amount, out number) ? number : 0;
    }

    public void SetColCount(string amount)
    {
        int number;
        GameData.colCount = int.TryParse(amount, out number) ? number : 0;
    }

    public void SetBombCount(string amount)
    {
        int number;
        GameData.bombCount = int.TryParse(amount, out number) ? number : 0;
    }
}

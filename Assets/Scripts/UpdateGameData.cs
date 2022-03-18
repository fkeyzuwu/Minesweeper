using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateGameData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rowCountText;
    [SerializeField] private TextMeshProUGUI colCountText;
    [SerializeField] private TextMeshProUGUI bombCountText;
    void Start()
    {
        rowCountText.text = GameData.rowCount.ToString();
        colCountText.text = GameData.colCount.ToString();
        bombCountText.text = GameData.bombCount.ToString();
    }

    public void IncreaseRowCount()
    {
        GameData.rowCount++;
        Debug.Log(GameData.rowCount);
        rowCountText.text = GameData.rowCount.ToString();
    }

    public void DecreaseRowCount()
    {
        GameData.rowCount--;
        rowCountText.text = GameData.rowCount.ToString();
    }

    public void IncreaseColCount()
    {
        GameData.colCount++;
        colCountText.text = GameData.colCount.ToString();
    }

    public void DecreaseColCount()
    {
        GameData.colCount--;
        colCountText.text = GameData.colCount.ToString();
    }

    public void IncreaseBombCount()
    {
        GameData.bombCount++;
        bombCountText.text = GameData.bombCount.ToString();
    }

    public void DecreaseBombCount()
    {
        GameData.bombCount--;
        bombCountText.text = GameData.bombCount.ToString();
    }
}

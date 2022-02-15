using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour , IPointerDownHandler ,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler , IPointerClickHandler
{
    public int row;
    public int col;

    public int bombsAround;
    public bool isBomb = false;
    public bool isFlagged = false;
    public bool isRevealed = false;
    [SerializeField] private GameObject blockContent;
    [SerializeField] public Image blockContentImage;
    [SerializeField] public Image blockImage;

    private bool isHovering = false;

    float lastTimeLeftClicked = 0;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isRevealed) return;

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            BlockInitializer.instance.FlagBlock(row, col);
        }
        //ui
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isRevealed && eventData.clickCount == 2)
        {
            //doubleclicked
            BlockInitializer.instance.RevealBlocks3x3(row, col);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isHovering) return;

        if (eventData.button == PointerEventData.InputButton.Right) return;

        if (isBomb)
        {
            BlockInitializer.instance.RevealAllBlocks(); //gameover
            return;
        }

        if (!isRevealed)
        {
            BlockInitializer.instance.RevealBlock(row, col);
        }
    }

    public void SetBlockContent(bool active)
    {
        blockContent.SetActive(active);
        isRevealed = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        //ui
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        //ui
    }
}
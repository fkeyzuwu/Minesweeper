using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour , IPointerDownHandler ,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int row;
    public int col;

    public int bombsAround;
    public bool isBomb = false;
    [SerializeField] private GameObject blockContent;
    [SerializeField] public Image blockImage;

    private bool isHovering = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        //ui
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

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isHovering) return;

        if (isBomb)
        {
            BlockInitializer.instance.RevealAllBlocks(); //gameover
            return;
        }

        BlockInitializer.instance.RevealBlock(row, col);
    }

    public void SetBlockContent(bool active)
    {
        blockContent.SetActive(active);
    }
}

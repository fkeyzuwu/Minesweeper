using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour , IPointerDownHandler ,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int bombsAround;
    public bool isBomb = false;
    [SerializeField] private GameObject blockContent;
    [SerializeField] public Image blockImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        //ui
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ui
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ui
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        blockContent.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{

    public bool IsEven = false;
    public enum TileColor
    {
        White,
        Green,
        Black,
        Red,
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        var image = GetComponent<Image>().color;
        if (image == Color.white)
        {
            image = Color.black;
        }
        else
        {
            image = Color.white;
        }

        GetComponent<Image>().color = image;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var image = GetComponent<Image>().color;
        if (image == Color.white)
        {
            image = Color.black;
        }
        else
        {
            image = Color.white;
        }
        GetComponent<Image>().color = image;

    }

}

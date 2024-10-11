using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    [Header("Debug")] [SerializeField] private IconDataWrapper data;

    public void SetIconData(IconDataWrapper idw)
    {
        data = idw;
        GetComponentInChildren<Image>().sprite = data.iconSprite;
    }
}
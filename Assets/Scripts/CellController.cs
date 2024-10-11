using System.Collections.Generic;
using Data;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Transform cellGround;

    [SerializeField] private GameObject tileObject;

    [Header("Debug")] public bool isPickable;
    public bool isOccupied;
    [SerializeField] Vector2Int coordinates;

    private void OnMouseDown()
    {
        if (!GameManager.instance.isLevelActive) return;
    }

    #region GETTERS & SETTERS

    public void SetIconData(IconDataWrapper idw)
    {
        GetComponentInChildren<TileController>().SetIconData(idw);
    }

    public void SetOccupied(GameObject tileObj)
    {
        tileObject = tileObj;
        isOccupied = true;
    }

    public void SetFree()
    {
        tileObject = null;
        isOccupied = false;
    }

    public GameObject GetTileObject()
    {
        return tileObject;
    }

    public Vector2Int GetCoordinates()
    {
        return coordinates;
    }

    #endregion
}
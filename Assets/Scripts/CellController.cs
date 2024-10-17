using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Serialization;

public class CellController : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Transform cellGround;

    [SerializeField] private GameObject tileObject;

    [Header("Debug")] public bool isOnCollection;
    public bool isOccupied;
    [SerializeField] Vector2Int coordinates;
    public List<CellController> upperCells;

    private void Start()
    {
        PickManager.instance.CellPickedEvent += OnACellPicked;
    }

    private void OnACellPicked(CellController pickedCell)
    {
        if (pickedCell == this) return;

        if (upperCells.Contains(pickedCell))
            upperCells.Remove(pickedCell);
        // refresh the neighbor cell list
    }

    private void OnMouseDown()
    {
        if (!GameManager.instance.isLevelActive) return;
        if (isOnCollection) return;
        if (!IsCellPickable()) return;

        PickManager.instance.TriggerCellPickedEvent(this);
        isOnCollection = true;
        gameObject.SetActive(false);
    }

    private bool IsCellPickable()
    {
        return upperCells.Count == 0;
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
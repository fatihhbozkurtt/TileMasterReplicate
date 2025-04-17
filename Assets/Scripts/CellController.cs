using System.Collections.Generic;
using Data;
using EssentialManagers.CustomPackages.SlotSystem.Scripts;
using EssentialManagers.Scripts;
using UnityEngine;
using Zenject;

public class CellController : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Transform cellGround;

    [SerializeField] private GameObject tileObject;

    [Header("Debug")] public bool isOnCollection;
    public bool isOccupied;
    [SerializeField] Vector2Int coordinates;
    public List<CellController> upperCells;
    private GameManager _gameManager;
    private SlotUser _slotUser;
    
    [Inject]
    private void ZenjectSetUp(GameManager gm)
    {
        _gameManager = gm;
    }

    private void Start()
    {
        PickManager.instance.CellPickedEvent += OnACellPicked;
        _slotUser = GetComponentInChildren<SlotUser>();
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
        if (!_gameManager.isLevelActive) return;
        if (isOnCollection) return;
        if (!IsCellPickable()) return;

        PickManager.instance.TriggerCellPickedEvent(this);
        isOnCollection = true;
        _slotUser.OnPicked();
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
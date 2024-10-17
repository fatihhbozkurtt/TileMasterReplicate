using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickManager : MonoSingleton<PickManager>
{
    public event System.Action<CellController> CellPickedEvent;

    public void TriggerCellPickedEvent(CellController cellController)
    {
        CellPickedEvent?.Invoke(cellController);
    }
}
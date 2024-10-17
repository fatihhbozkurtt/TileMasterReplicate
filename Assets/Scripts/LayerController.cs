using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    public List<CellController> cells;

    private void Awake()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].gameObject.activeInHierarchy) continue;
            cells.Remove(cells[i]);
            Destroy(cells[i].gameObject);
        }
    }
}
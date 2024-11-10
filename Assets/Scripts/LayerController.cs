using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    public List<CellController> cells;

    private void Awake()
    {
        cells = GetComponentsInChildren<CellController>().ToList();
        
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].gameObject.activeInHierarchy) continue;
            cells.Remove(cells[i]);
            Destroy(cells[i].gameObject);
        }
    }
}
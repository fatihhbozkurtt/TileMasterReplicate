using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpperCollider : MonoBehaviour
{
    private CellController parentCell;
    [SerializeField] private List<CellController> upperCells = new();

    private IEnumerator Start()
    {
        parentCell = transform.parent.GetComponent<CellController>();

        yield return new WaitForSeconds(1);
        parentCell.upperCells = upperCells;
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.transform.TryGetComponent(out CellController upperCell)) return;
        if (upperCells.Contains(upperCell)) return;
        if (parentCell != upperCell)
            upperCells.Add(upperCell);
    }
}
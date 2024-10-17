using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpperCollider : MonoBehaviour
{
    private CellController parentCell;
    private List<CellController> upperCells = new();

    private IEnumerator Start()
    {
        parentCell = transform.parent.GetComponent<CellController>();

        yield return new WaitForSeconds(1);
        parentCell.upperCells = upperCells;
        Debug.Log("Cell count " + upperCells.Count);
        gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out CellController upperCell))
        {
            if (!upperCells.Contains(upperCell))
                upperCells.Add(upperCell);
        }
    }
}
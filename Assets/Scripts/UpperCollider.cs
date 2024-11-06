using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperCollider : MonoBehaviour
{
    private CellController _parentCell;
    [SerializeField] private List<CellController> upperCells = new();

    private IEnumerator Start()
    {
        _parentCell = transform.parent.GetComponent<CellController>();

        yield return new WaitForSeconds(1);
        _parentCell.upperCells = upperCells;
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.transform.TryGetComponent(out CellController upperCell)) return;
        if (upperCells.Contains(upperCell)) return;
        if (_parentCell != upperCell)
            upperCells.Add(upperCell);
    }
}
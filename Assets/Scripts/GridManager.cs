using System.Collections.Generic;
using UnityEngine;
using Data;

public class GridManager : MonoSingleton<GridManager>
{
    [Header("References")] public List<LayerController> layers;
    public IconSo iconSo;
    [Header("Debug")] [SerializeField] private List<CellController> activeCells;
    private List<IconDataWrapper> _iconDataList; // 10 farklı iconData türü


    protected override void Awake()
    {
        base.Awake();
        _iconDataList = iconSo.iconDataWrappers;
        foreach (var l in layers)
        {
            foreach (var c in l.cells)
            {
                if (!activeCells.Contains(c))
                    activeCells.Add(c);
            }
        }

        ShuffleList(activeCells);
        ShuffleList(_iconDataList);
        AssignIconData();
    }

    private void AssignIconData()
    {
        if (_iconDataList == null || _iconDataList.Count < 10 || activeCells == null || activeCells.Count == 0)
        {
            Debug.LogError("IconData veya activeCells düzgün bir şekilde tanımlanmamış.");
            return;
        }

        // Minimum 3 cellController için her iconData'nın ataması yapılacak.
        int minAssignCount = 3;
        int totalCells = activeCells.Count;
        int iconDataIndex = 0;

        // Her iconData'nın en az 3 kere atanacağı şekilde döngü
        for (int i = 0; i < totalCells; i++)
        {
            CellController cell = activeCells[i];

            // Mevcut iconData'yı ata
            cell.SetIconData(_iconDataList[iconDataIndex]);

            // Her 3 cell'den sonra yeni bir iconData kullan
            if ((i + 1) % minAssignCount == 0)
            {
                iconDataIndex++;

                // Eğer iconDataIndex 10'dan büyükse, tekrar başa döner
                if (iconDataIndex >= _iconDataList.Count)
                {
                    iconDataIndex = 0;
                }
            }
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        var rng = new System.Random();
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}
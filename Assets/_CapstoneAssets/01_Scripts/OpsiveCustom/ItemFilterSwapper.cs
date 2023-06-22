using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateInventorySystem;
using Opsive.UltimateInventorySystem.UI.Panels.ItemViewSlotContainers.GridFilterSorters.InventoryGridFilters;
using Opsive.UltimateInventorySystem.UI.Panels.ItemViewSlotContainers.GridFilterSorters;
public class ItemFilterSwapper : MonoBehaviour
{

    [SerializeField] private ItemInfoCategoryFilter Filter1;
    [SerializeField] private ItemInfoCategoryFilter Filter2;

    [SerializeField] private ItemInfoMultiFilterSorter FilterSorter;

    public void RemoveItemFilters()
    {
        FilterSorter.GridFilters.Clear();
    }

    public void AddItemFilters()
    {
        FilterSorter.GridFilters.Add(Filter1);
        FilterSorter.GridFilters.Add(Filter2);
    }

}

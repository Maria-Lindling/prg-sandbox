using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryItemEntry", order = 1)]
public class InventoryItemEntryScriptableObject : ScriptableObject
{
    [SerializeField] private InventoryItemScriptableObject item;
    [SerializeField] private int quantity;

    private GameObject _representedBy;
    /// <summary>
    /// The GameObject that this inventoryItem is represented by.
    /// </summary>
    public GameObject RepresentedBy { get => _representedBy; set => _representedBy = value; }

    private int _quantity;
    public int Quantity { get => _quantity; set => _quantity = value; }
    public InventoryItemScriptableObject InventoryItem => item;

    private void OnEnable()
    {
        _quantity = quantity;
    }

    public bool TryMergeStack(InventoryItemEntryScriptableObject inventoryItemEntry)
    {
        if(
            RepresentedBy != inventoryItemEntry.RepresentedBy &&
            InventoryItem.ItemName == inventoryItemEntry.InventoryItem.ItemName
        ) {
            _quantity += inventoryItemEntry.Quantity;
            Destroy(inventoryItemEntry.RepresentedBy);
            RepresentedBy.GetComponent<InventoryItemDataShell>().SetItemCountField(Quantity);
            return true;
        }
        return false;
    }
}

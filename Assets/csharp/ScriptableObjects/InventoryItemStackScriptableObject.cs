using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
/// Note that items should always be in a stack, even if the maximum
/// stack size is one and even if the quantity is one or less.
/// </remarks>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryItemStack", order = 1)]
public class InventoryItemStackScriptableObject : ScriptableObject
{
    [SerializeField] private InventoryItemScriptableObject item;
    [SerializeField] private int quantity;

    /// <summary>
    /// The GameObject that this inventoryItem is represented by.
    /// </summary>
    public GameObject RepresentedBy { get; set; }

    private int _quantity;
    /// <summary>
    /// The number of items in this stack.
    /// </summary>
    public int Quantity { get => _quantity; set => _quantity = value; }
    public InventoryItemScriptableObject InventoryItem => item;

    public string SourceInventory { get; set; }

    /// <summary>
    /// Assigns the persistent serialized value to an runtime-only field,
    /// preventing it from carrying back over to the ScriptableObject.
    /// </summary>
    private void OnEnable()
    {
        _quantity = quantity;
    }

    /// <summary>
    /// Attempts to combine another stack of items with this one.
    /// </summary>
    /// <returns>
    /// True if both the stack being merged with is not this
    /// same stack and the stack shares an ItemName with this
    /// stack.
    /// </returns>
    public bool TryMergeStack(InventoryItemStackScriptableObject inventoryItemEntry)
    {
        if(
            RepresentedBy != inventoryItemEntry.RepresentedBy &&
            InventoryItem.ItemName == inventoryItemEntry.InventoryItem.ItemName
        ) {
            _quantity += inventoryItemEntry.Quantity;
            inventoryItemEntry.RepresentedBy.SetActive(false);
            RepresentedBy.GetComponent<InventoryItemDataShell>().SetItemCountField(Quantity);
            return true;
        }
        return false;
    }
}

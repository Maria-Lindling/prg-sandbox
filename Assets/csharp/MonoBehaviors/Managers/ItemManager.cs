using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour, IItemManager
{
    [SerializeField] private GameObject inventoryListEntryPrefab;
    [SerializeField] private List<InventoryItemStackScriptableObject> inventoryItems;

    protected GameObject _inventoryListEntryPrefab;
    protected List<InventoryItemStackScriptableObject> _inventoryItems;

    public List<InventoryItemStackScriptableObject> InventoryItems => _inventoryItems;

    public void Start()
    {
        _inventoryListEntryPrefab = inventoryListEntryPrefab;
        _inventoryItems = inventoryItems;
    }

    /// <summary>
    /// Fill the inventory menu with the items assigned to it in the
    /// unity development GUI.
    /// </summary>
    protected virtual void Populate()
    { }

    /// <summary>
    /// This method is too different between the player character
    /// inventory an container inventories to meaningfully share
    /// a base, but both necessarily must feature it.
    /// </summary>
    public virtual void ToggleItemMenu() { }


    #region IItemManager Interface Implementation
    /// <remarks>converted to DUMMY due to usage of layoutGroup</remarks>
    public virtual void AddItemEntry(InventoryItemStackScriptableObject itemEntry)
    {
        /*if (!inventoryItems.Any(extantEntry => extantEntry.TryMergeStack(itemEntry)))
        {
            inventoryItems.Add(itemEntry);
            itemEntry.RepresentedBy.GetComponent<RectTransform>().SetParent(layoutGroup);
        }*/
    }

    /// <summary>
    /// Collapses any duplicate item entries into a single item entry that
    /// with a quantity composed of all item quantities.
    /// </summary>
    /// <remarks>
    /// This is an O(n) operation, but it's straightforward to implement.
    /// This is for edge-cases and "emergencies", since normally the
    /// AddItemEntry method already merges items with the same key (name).
    /// </remarks>
    public void MergeDuplicates() =>
        inventoryItems.ForEach(firstEntry => inventoryItems.ForEach(secondEntry => firstEntry.TryMergeStack(secondEntry)));

    /// <summary>
    /// Ensures all items stored in this ItemManager actually are children
    /// of the layoutGroup belonging to this ItemManager.
    /// </summary>
    /// <remarks>converted to DUMMY due to usage of layoutGroup</remarks>
    public void Synchronize() =>
        inventoryItems.ForEach(iie => /*iie.RepresentedBy.GetComponent<RectTransform>().SetParent(layoutGroup)*/ { return; });
    #endregion
}

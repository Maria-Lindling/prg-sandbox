using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemManager
{
    public List<InventoryItemEntryScriptableObject> InventoryItems { get; }
    public void AddItemEntry(InventoryItemEntryScriptableObject itemEntry);
    public void MergeDuplicates();
    public void Synchronize();
}

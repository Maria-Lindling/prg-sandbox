using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemManager
{
    public List<InventoryItemStackScriptableObject> InventoryItems { get; }
    public void AddItemEntry(InventoryItemStackScriptableObject itemEntry);
    public void MergeDuplicates();
    public void Synchronize();
}

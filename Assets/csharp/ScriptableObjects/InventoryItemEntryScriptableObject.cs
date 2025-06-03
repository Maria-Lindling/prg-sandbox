using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryItemEntry", order = 1)]
public class InventoryItemEntryScriptableObject : ScriptableObject
{
    [SerializeField] private InventoryItemScriptableObject item;
    [SerializeField] private int quantity;

    public int Quantity => quantity;
    public InventoryItemScriptableObject InventoryItem => item;
}

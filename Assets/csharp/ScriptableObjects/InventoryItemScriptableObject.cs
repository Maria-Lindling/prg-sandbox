using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class InventoryItemScriptableObject : BaseItem
{
    private GameObject _representedBy;

    public GameObject RepresentedBy { get => _representedBy; set => _representedBy = value; }
}

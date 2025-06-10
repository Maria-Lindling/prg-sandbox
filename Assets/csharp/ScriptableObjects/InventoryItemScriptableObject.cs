using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
/// InventoryItemScriptableObject is distinct from BaseItem in spite of possessing no
/// unique qualities of its own, for the reason that non-item "items" may share the
/// BaseItem parent class, while being distinct from being an "inventory item".
/// (also see: status effects as "items") A full implementation would probably use
/// interfaces here, but since I only have one type of item right now, I'm not
/// doing that.
/// </remarks>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class InventoryItemScriptableObject : BaseItem
{
}

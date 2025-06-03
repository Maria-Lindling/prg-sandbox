using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject
{
    // copied BaseItem flags from [1f44b2b]
    #region Copied Flags
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    #endregion

    /// <summary>
    /// Per how many items (usually 1) the itemUnitPrice is.
    /// "itemUnitQuantity 5" means the itemPrice refers to a bundle of 5
    /// </summary>
    [SerializeField] private int itemUnitQuantity;

    #region Copied Flags
    [SerializeField] private int itemUnitPrice;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private bool isConsumable;
    #endregion

    /// <summary>
    /// Can the item be equipped.
    /// </summary>
    [SerializeField] private bool isEquippable;

    #region Copied Flags
    [SerializeField] private bool isEquipped;
    [SerializeField] private bool isUsableInBattle;
    [SerializeField] private bool isUsableOutsideBattle;
    [SerializeField] private bool isStackable;
    #endregion

    /// <summary>
    /// How many items are held by a single stack.
    /// </summary>
    [SerializeField] private int itemStackSize;

    #region Copied Flags
    [SerializeField] private bool isSellable;
    [SerializeField] private bool isCraftable;
    [SerializeField] private bool isTradable;
    [SerializeField] private bool isDestroyable;
    [SerializeField] private bool isQuestItem;
    #endregion
}

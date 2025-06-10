using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItemDataShell : MonoBehaviour
{
    [SerializeField] private TMP_Text nameField;
    [SerializeField] private TMP_Text itemCountField;

    /// <summary>
    /// Set the item name being displayed by this inventory listing entry.
    /// </summary>
    public InventoryItemDataShell SetNameField(string value)
    {
        nameField.text = value;
        return this;
    }

    /// <summary>
    /// Set the item-count being displayed by this inventory listing entry.
    /// </summary>
    public InventoryItemDataShell SetItemCountField(int value)
    {
        itemCountField.text = $"{value}";
        return this;
    }
}

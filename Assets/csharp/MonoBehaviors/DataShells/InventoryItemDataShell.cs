using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItemDataShell : MonoBehaviour
{
    [SerializeField] private TMP_Text nameField;
    [SerializeField] private TMP_Text itemCountField;

    public InventoryItemDataShell SetNameField(string value)
    {
        nameField.text = value;
        return this;
    }

    public InventoryItemDataShell SetItemCountField(int value)
    {
        itemCountField.text = $"{value}";
        return this;
    }
}

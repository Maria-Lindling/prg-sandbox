using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItemDataShell : MonoBehaviour
{
    private static int _indexIterable = -1;
    private static int NextIndex { get { _indexIterable++;  return _indexIterable; } }
    public int Index;


    [SerializeField] private TMP_Text nameField;
    [SerializeField] private TMP_Text itemCountField;

    private void Start()
    {
        Index = NextIndex;
    }

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

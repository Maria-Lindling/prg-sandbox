using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

/// <remarks>
/// Re-doing this I'd probably make an ItemManager base class which
/// implements the IItemManager interface.
/// </remarks>
public class ItemMenuManager : MonoBehaviour, IItemManager
{
    private static ItemMenuManager _instance;
    public static ItemMenuManager Instance { get => _instance; private set => _instance = value; }

    [SerializeField] private GameObject itemMenuUI;
    [SerializeField] private GameObject tooltipBox;
    [SerializeField] private RectTransform layoutGroup;
    [SerializeField] private GameObject inventoryListEntryPrefab;
    [SerializeField] private List<InventoryItemEntryScriptableObject> inventoryItems;


    #region Public Facing Properties
    public GameObject ItemMenuUI => itemMenuUI;
    public Canvas InventoryCanvas => itemMenuUI.GetComponent<Canvas>();
    public ItemContainerManager ActiveContainer { get; set; }
    public List<InventoryItemEntryScriptableObject> InventoryItems => inventoryItems;
    #endregion


    public void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            itemMenuUI.SetActive(false);
            tooltipBox.SetActive(false);
            ActiveContainer = null;
        }
        Populate();
    }

    /// <summary>
    /// Fill the inventory menu with the items assigned to it in the
    /// unity development GUI.
    /// </summary>
    private void Populate()
    {
        inventoryItems.ForEach((ii) => {

            ii.RepresentedBy = Instantiate(inventoryListEntryPrefab, layoutGroup);

            ii.RepresentedBy.name = ii.InventoryItem.name;

            ii.RepresentedBy.GetComponent<InventoryItemDataShell>()
                .SetNameField(ii.InventoryItem.ItemName)
                .SetItemCountField(ii.Quantity);

            ii.RepresentedBy.GetComponent<InventoryDragScript>()
                .SetCanvas(InventoryCanvas.GetComponent<Canvas>());
        });
    }

    /// <summary>
    /// Open/Close the item menu, also resetting information such as
    /// the ActiveContainer, as well as setting/releasing the InputLock
    /// placed on player movement.
    /// </summary>
    public void ToggleItemMenu()
    {
        if( ActiveContainer != null)
        {
            ActiveContainer.ToggleItemMenu();
            ActiveContainer = null;
        }
        itemMenuUI.SetActive(!itemMenuUI.activeSelf);
        BaseCharacterController.Instance.InputLock = itemMenuUI.activeSelf;
    }


    #region IItemManager Interface Implementation
    public void AddItemEntry(InventoryItemEntryScriptableObject itemEntry)
    {
        if (!inventoryItems.Any(extantEntry => extantEntry.TryMergeStack(itemEntry)))
        {
            inventoryItems.Add(itemEntry);
            itemEntry.RepresentedBy.GetComponent<RectTransform>().SetParent(layoutGroup);
        }
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
        inventoryItems.ForEach(firstEntry => inventoryItems.ForEach(secondEntry=>firstEntry.TryMergeStack(secondEntry)));

    /// <summary>
    /// Ensures all items stored in this ItemManager actually are children
    /// of the layoutGroup belonging to this ItemManager.
    /// </summary>
    public void Synchronize() =>
        inventoryItems.ForEach(iie => iie.RepresentedBy.GetComponent<RectTransform>().SetParent(layoutGroup));
    #endregion
}

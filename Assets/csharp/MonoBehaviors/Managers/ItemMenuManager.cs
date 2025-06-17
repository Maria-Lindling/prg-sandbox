using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <remarks>
/// Re-doing this I'd probably make an ItemManager base class which
/// implements the IItemManager interface.
/// </remarks>
public class ItemMenuManager : ItemManager
{
    #region Player Character Instance
    private static ItemMenuManager _instance;
    public static ItemMenuManager Instance { get => _instance; private set => _instance = value; }
    #endregion

    [SerializeField] private RectTransform layoutGroupInventory;
    [SerializeField] private GameObject itemMenuUI;

    [SerializeField] private RectTransform layoutGroupContainer;
    [SerializeField] private GameObject containerPanelUI;
    [SerializeField] private GameObject containerPanelBG;

    [SerializeField] private GameObject tooltipBox;


    #region Public Facing Properties
    //public GameObject ItemMenuUI => itemMenuUI;
    public GameObject ContainerPanelUI => containerPanelUI;
    public GameObject ContainerPanelBG => containerPanelBG;
    public RectTransform ContainerLayoutGroup => layoutGroupContainer;
    public Canvas InventoryCanvas => itemMenuUI.GetComponent<Canvas>();
    public ItemContainerManager ActiveContainer { get; set; }
    #endregion


    public new void Start()
    {
        base.Start();

        if (_instance == null)
        {
            _instance = this;
            itemMenuUI.SetActive(false);
            
            tooltipBox.SetActive(false);

            ContainerPanelUI.SetActive(false);
            ContainerPanelBG.SetActive(false);

            ActiveContainer = null;
        }
        Populate();
    }

    #region overrides: ItemManager 
    protected override void Populate()
    {
        base.Populate();

        _inventoryItems.ForEach((ii) => {

            ii.RepresentedBy = Instantiate(_inventoryListEntryPrefab, layoutGroupInventory);

            ii.RepresentedBy.name = ii.InventoryItem.name;

            ii.SourceInventory = "PlayerInventory" ;

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
    public override void ToggleItemMenu()
    {
        base.ToggleItemMenu();

        if( ActiveContainer != null)
        {
            ActiveContainer.ToggleItemMenu();
            ActiveContainer = null;
        }
        itemMenuUI.SetActive(!itemMenuUI.activeSelf);
        BaseCharacterController.Instance.InputLock = itemMenuUI.activeSelf;
    }
    #endregion

    public override void AddItemEntry(InventoryItemStackScriptableObject itemEntry)
    {
        if (!_inventoryItems.Any(extantEntry => extantEntry.TryMergeStack(itemEntry)))
        {
            _inventoryItems.Add(itemEntry);
            itemEntry.RepresentedBy.GetComponent<RectTransform>().SetParent(layoutGroupInventory);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.InputSystem.InputAction;

public class ItemContainerManager : ItemManager, IInteractible
{
    private static List<ItemContainerManager> Instances = new List<ItemContainerManager>();

    public string InventoryName { get; private set; }

    /// <summary>
    /// Instantiates the container inventory, ensuring though InventoryName that inventories
    /// are re-used instead of duplicated if the location containing them is re-entered.
    /// </summary>
    public new void Start()
    {
        base.Start();

        InventoryName = gameObject.name;

        if (ItemContainerManager.Instances.Any(icm=>icm.InventoryName == InventoryName))
            return;

        Populate();

        ItemContainerManager.Instances.Add(this);
    }

    #region overrides: ItemManager 
    protected override void Populate()
    {
        base.Populate();

        _inventoryItems.ForEach((ii) => {

            ii.RepresentedBy = Instantiate(_inventoryListEntryPrefab, ItemMenuManager.Instance.ContainerLayoutGroup);

            ii.RepresentedBy.name = ii.InventoryItem.name;

            ii.SourceInventory = InventoryName;

            ii.RepresentedBy.GetComponent<InventoryItemDataShell>()
                .SetNameField(ii.InventoryItem.ItemName)
                .SetItemCountField(ii.Quantity);

            ii.RepresentedBy.GetComponent<InventoryDragScript>()
                .SetCanvas(ItemMenuManager.Instance.InventoryCanvas.GetComponent<Canvas>());
        });
    }

    public override void ToggleItemMenu()
    {
        base.ToggleItemMenu();

        ItemMenuManager.Instance.ContainerPanelUI.SetActive(!ItemMenuManager.Instance.ContainerPanelUI.activeSelf);
        ItemMenuManager.Instance.ContainerPanelBG.SetActive( ItemMenuManager.Instance.ContainerPanelUI.activeSelf);

        if (ItemMenuManager.Instance.ContainerPanelUI.activeSelf)
        {
            _inventoryItems.ForEach((ii) => { if(ii.RepresentedBy != null && ii.SourceInventory == InventoryName) ii.RepresentedBy.SetActive(true); });
            ItemMenuManager.Instance.ToggleItemMenu();
            ItemMenuManager.Instance.ActiveContainer = this;
        }
        else
        {
            Depopulate();
            Activate();
        }
    }
    #endregion

    private void Depopulate()
    {
        _inventoryItems.ForEach((ii) => { if (ii.RepresentedBy != null) ii.RepresentedBy.SetActive(false); });
    }

    #region interface: IInteractible

    public void Activate()
    {
        InputAction interactAction = BaseCharacterController.Instance.PlayerInput.actions["Interaction"];
        interactAction.performed += Interact;
        interactAction.canceled += Interact;
    }

    public void Deactivate()
    {
        InputAction interactAction = BaseCharacterController.Instance.PlayerInput.actions["Interaction"];
        interactAction.performed -= Interact;
        interactAction.canceled -= Interact;
    }

    public void Interact(CallbackContext ctx)
    {
        if( ctx.action.IsPressed() )
        {
            GetComponent<AudioSource>().Play();

            Deactivate();

            ToggleItemMenu();
        }
    }
    #endregion
    public override void AddItemEntry(InventoryItemStackScriptableObject itemEntry)
    {
        if (!_inventoryItems.Any(extantEntry => extantEntry.TryMergeStack(itemEntry)))
        {
            _inventoryItems.Add(itemEntry);
            itemEntry.RepresentedBy.GetComponent<RectTransform>().SetParent(ItemMenuManager.Instance.ContainerLayoutGroup);
        }
    }
}

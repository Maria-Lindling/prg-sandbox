using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.InputSystem.InputAction;

public class ItemContainerManager : MonoBehaviour, IItemManager
{
    [SerializeField] private GameObject containerPanelUI;
    [SerializeField] private GameObject containerPanelBG;
    [SerializeField] private RectTransform layoutGroup;
    [SerializeField] private GameObject inventoryListEntryPrefab;
    [SerializeField] private List<InventoryItemEntryScriptableObject> inventoryItems;

    public List<InventoryItemEntryScriptableObject> InventoryItems => inventoryItems;
    public void Start()
    {
        containerPanelUI.SetActive(false);
        containerPanelBG.SetActive(false);
    }

    private void Populate()
    {
        inventoryItems.ForEach((ii) => {

            ii.RepresentedBy = Instantiate(inventoryListEntryPrefab, layoutGroup);

            ii.RepresentedBy.name = ii.InventoryItem.name;

            ii.RepresentedBy.GetComponent<InventoryItemDataShell>()
                .SetNameField(ii.InventoryItem.ItemName)
                .SetItemCountField(ii.Quantity);

            ii.RepresentedBy.GetComponent<InventoryDragScript>()
                .SetCanvas(ItemMenuManager.Instance.InventoryCanvas.GetComponent<Canvas>());

        });
    }

    private void Depopulate()
    {
        inventoryItems.ForEach((ii) => Destroy(ii.RepresentedBy));
    }

    public void ToggleItemMenu()
    {
        containerPanelUI.SetActive(!containerPanelUI.activeSelf);
        containerPanelBG.SetActive( containerPanelUI.activeSelf);
        if (containerPanelUI.activeSelf)
        {
            Populate();
            ItemMenuManager.Instance.ToggleItemMenu();
            ItemMenuManager.Instance.ActiveContainer = this;
        }
        else
        {
            Depopulate();
            InputAction interactAction = BaseCharacterController.Instance.PlayerInput.actions["Interaction"];
            interactAction.performed += Interact;
            interactAction.canceled += Interact;
        }
    }

    public void Interact(CallbackContext ctx)
    {
        if( ctx.action.IsPressed() )
        {
            InputAction interactAction = BaseCharacterController.Instance.PlayerInput.actions["Interaction"];
            interactAction.performed -= Interact;
            interactAction.canceled -= Interact;

            ToggleItemMenu();
        }
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

    public void MergeDuplicates() =>
        inventoryItems.ForEach(firstEntry => inventoryItems.ForEach(secondEntry => firstEntry.TryMergeStack(secondEntry)));

    public void Synchronize() =>
        inventoryItems.ForEach((iie) => iie.RepresentedBy.GetComponent<RectTransform>().SetParent(layoutGroup));
    #endregion
}

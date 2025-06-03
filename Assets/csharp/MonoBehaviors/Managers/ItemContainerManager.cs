using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.InputSystem.InputAction;

public class ItemContainerManager : MonoBehaviour
{
    [SerializeField] private GameObject containerPanelUI;
    [SerializeField] private RectTransform layoutGroup;
    [SerializeField] private GameObject inventoryListEntryPrefab;
    [SerializeField] private List<InventoryItemEntryScriptableObject> inventoryItems;

    public List<InventoryItemEntryScriptableObject> InventoryItems => inventoryItems;
    public void Start()
    {
        containerPanelUI.SetActive(false);
    }

    private void Populate()
    {
        inventoryItems.ForEach((ii) => {

            GameObject go = Instantiate(inventoryListEntryPrefab, layoutGroup);

            go.GetComponent<InventoryItemDataShell>()
                .SetNameField(ii.InventoryItem.name)
                .SetItemCountField(ii.Quantity);

            go.GetComponent<InventoryDragScript>()
                .SetCanvas(ItemMenuManager.Instance.InventoryCanvas.GetComponent<Canvas>());

            ii.InventoryItem.RepresentedBy = go;
        });
    }

    private void Depopulate()
    {
        inventoryItems.ForEach((ii) => Destroy(ii.InventoryItem.RepresentedBy));
    }

    public void ToggleItemMenu()
    {
        containerPanelUI.SetActive(!containerPanelUI.activeSelf);
        if(containerPanelUI.activeSelf)
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
}

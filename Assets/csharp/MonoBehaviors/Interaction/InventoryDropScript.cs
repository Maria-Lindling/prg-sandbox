using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class InventoryDropScript : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// Attempts to verify which container (Inventory/Container) the
    /// item being dragged is located in. Then set that one as the
    /// source of the transfer and the other as the target. Then
    /// the logical item and the GameObject representing it are
    /// transferred to their respective new parents.
    /// </summary>
    /// <remarks>
    /// This is really clunky and I'd rather do it generically,
    /// but that'd require yet another layer of abstraction that's
    /// probably just further outside the scope of this assignment.
    /// </remarks>
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (ItemMenuManager.Instance.ActiveContainer == null)
        {
            Debug.Log("The dragged item has nowhere to go.");
            // The dragged item has nowhere to go.
            return;
        }

        if (!eventData.hovered.Any(go => go == gameObject))
        {
            Debug.Log("This container isn't the target of the drop.");
            // This container isn't the target of the drop.
            return;
        }

        IItemManager sourceInventory = null;
        IItemManager targetInventory = null;

        if (TryGetItem(eventData.pointerDrag, ItemMenuManager.Instance.ActiveContainer.InventoryItems, out InventoryItemEntryScriptableObject dragItem))
        {
            //Debug.Log($"Moving {eventData.pointerDrag.name} from container to inventory {name}.");
            sourceInventory = ItemMenuManager.Instance.ActiveContainer;
            targetInventory = ItemMenuManager.Instance;
            
        }
        else if (TryGetItem(eventData.pointerDrag, ItemMenuManager.Instance.InventoryItems, out dragItem))
        {
            //Debug.Log($"Moving {eventData.pointerDrag.name} from inventory to container {name}.");
            sourceInventory = ItemMenuManager.Instance;
            targetInventory = ItemMenuManager.Instance.ActiveContainer;
        }
        else
        {
            Debug.Log($"The item \"{eventData.pointerDrag.name}\" was found in neither the container nor the inventory.");
            // The item wasn't found in neither the container nor the inventory.
            return;
        }

        TransferItem(dragItem, sourceInventory, targetInventory);
    }

    /// <summary>
    /// Attempts to find the logical item being dropped onto
    /// the object that contains this script.
    /// </summary>
    /// <returns>The logical item represented by the item being dropped.</returns>
    private InventoryItemEntryScriptableObject GetItem(
        GameObject pointerDrag,
        List<InventoryItemEntryScriptableObject> inventoryItems) =>
            inventoryItems
                .Where(iie => {
                    int pointerIndex = pointerDrag.GetComponent<InventoryItemDataShell>().Index;
                    int entryIndex = iie.RepresentedBy.GetComponent<InventoryItemDataShell>().Index;
                    return (pointerIndex == entryIndex);
                })
                    .FirstOrDefault();

    /// <summary>
    /// Attempts to find the logical item being dropped onto
    /// the object that contains this script.
    /// </summary>
    /// <returns>Whether or not the item could be found.</returns>
    private bool TryGetItem(
        GameObject pointerDrag,
        List<InventoryItemEntryScriptableObject> inventoryItems,
        out InventoryItemEntryScriptableObject gameObject)
    {
        gameObject = GetItem(pointerDrag,inventoryItems);
        //Debug.Log($"Found: {gameObject?.name ?? "null"}");
        return gameObject != null;
    }

    /// <summary>
    /// Transfers the inventoryItem from the source to its
    /// destination/target.
    /// </summary>
    private void TransferItem(InventoryItemEntryScriptableObject inventoryItem, IItemManager sourceInventory, IItemManager targetInventory)
    {
        targetInventory.AddItemEntry(inventoryItem);
        sourceInventory.InventoryItems.Remove(inventoryItem);
    }
}

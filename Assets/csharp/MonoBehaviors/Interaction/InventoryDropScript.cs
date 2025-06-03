using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDropScript : MonoBehaviour, IDropHandler
{
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        RectTransform rt = eventData.pointerDrag.GetComponent<RectTransform>();


        InventoryItemEntryScriptableObject iie = ItemMenuManager.Instance
            .ActiveContainer
                .InventoryItems
                    .Find((ii) => (ii.InventoryItem.RepresentedBy == eventData.pointerDrag));

        ItemMenuManager.Instance.InventoryItems.Add(iie);

        ItemMenuManager.Instance.ActiveContainer.InventoryItems.Remove(iie);

        rt.SetParent(GetComponent<RectTransform>());

        //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    }
}

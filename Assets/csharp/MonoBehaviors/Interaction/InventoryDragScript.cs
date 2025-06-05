using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 returnAnchor;

    [SerializeField] private Canvas canvas;

    /// <summary>
    /// Save the initial position of the item and then offset the
    /// item to be clear of the pointer.
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        returnAnchor = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition += rectTransform.sizeDelta/3 + Vector2.one;
    }

    /// <summary>
    /// Move the display-position of the dragged item along with the
    /// movement of the pointer.
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    /// <summary>
    /// Reset the dragged item's position back to its initial position.
    /// The InventoryDropScript takes place after this, so the new
    /// position gained from re-parenting takes precedence.
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = returnAnchor;
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Set the canvas being used. The purpose of this method is likely
    /// redundant with pre-existing systems, but here its job is to
    /// syncronize reference for which canvas is being used.
    /// </summary>
    public InventoryDragScript SetCanvas(Canvas canvas)
    {
        this.canvas = canvas;
        return this;
    }
}

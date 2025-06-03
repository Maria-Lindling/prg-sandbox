using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ItemMenuManager : MonoBehaviour
{
    private static ItemMenuManager _instance;
    public static ItemMenuManager Instance { get => _instance; private set => _instance = value; }

    [SerializeField] private GameObject itemMenuUI;
    [SerializeField] private RectTransform layoutGroup;
    [SerializeField] private GameObject inventoryListEntryPrefab;
    [SerializeField] private List<InventoryItemEntryScriptableObject> inventoryItems;


    public GameObject ItemMenuUI => itemMenuUI;
    public Canvas InventoryCanvas => itemMenuUI.GetComponent<Canvas>();
    public ItemContainerManager ActiveContainer { get; set; }
    public List<InventoryItemEntryScriptableObject> InventoryItems => inventoryItems;

    public void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            itemMenuUI.SetActive(false);
            ActiveContainer = null;
        }
        Populate();
    }

    private void Populate()
    {
        inventoryItems.ForEach((ii) => {

            GameObject go = Instantiate(inventoryListEntryPrefab, layoutGroup);

            go.GetComponent<InventoryItemDataShell>()
                .SetNameField(ii.InventoryItem.name)
                .SetItemCountField(ii.Quantity);

            go.GetComponent<InventoryDragScript>()
                .SetCanvas(InventoryCanvas.GetComponent<Canvas>());

            ii.InventoryItem.RepresentedBy = go;
        });
    }

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
}

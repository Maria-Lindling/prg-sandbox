## maria_wickes_PRGAbgabe01.unitypackage

External Dependencies:
- 2D Tilemap Editor
- Cinemachine
- InputSystem
- TextMesh Pro
- Unity UI
- Universal RP

Keymaps:
- Movement: WASD
- Interact: E
- Open/Close Inventory: I

Demo Path:
- Begin in the Scene "Farm"
- Exit east to "Town"
- Enter the first house to the Player Character's left by Walking into the door, taking you to "TownInteriors".
- In "TownInteriors" approach the chest(s) and press E to open them.
- [[ Audio should Play as you interact with the chest(s). ]]
- Manipulates the items as you wish, taking/leaving/stacking them.
- [[ Here ends the guided demo segment. ]]
- [[ Inventories should persist and continue to function across scenes. ]]

Important inventory-relevant source code files:
- csharp/Interfaces/IItemMenu.cs
- csharp/Interfaces/IInteractible.cs
- csharp/MonoBehaviors/Managers/ItemManager.cs
- csharp/MonoBehaviors/Managers/ItemMenuManager.cs
- csharp/MonoBehaviors/Managers/ItemContainerManager.cs
- csharp/MonoBehaviors/Interaction/InventoryDragScript.cs
- csharp/MonoBehaviors/Interaction/InventoryDropScript.cs
- csharp/ScriptableObjects/InventoryItemScriptableObject.cs
- csharp/ScriptableObjects/InventoryItemStackScriptableObject.cs

Other relevant source code file:
- csharp/MonoBehaviors/BaseCharacterController.cs
- csharp/MonoBehaviors/Managers/PersistanceManager.cs

Important inventory-relevant files:
- Prefabs/ListItem.prefab
- ScriptableObjects/Items/**
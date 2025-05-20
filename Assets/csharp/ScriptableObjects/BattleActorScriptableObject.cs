using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BattleActor", order = 1)]
public class BattleActorScriptableObject : ScriptableObject
{
    //start     example code
    public string prefabName;

    public GameObject spawnablePrefab;

    public int numberOfPrefabsToCreate;

    public int experiencePoints;

    public int maxHealth;

    public Vector3[] spawnPoints;

    //end       example

    private Dictionary<string, bool> _equipment;
    private Dictionary<string, int> _items;


    public int ExperiencePoints { get; private set; }
    public int Level { get; private set; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }


    public Dictionary<string, bool> Equipment { get => _equipment; }
    public Dictionary<string, int> Items { get => _items; }



    // This will be appended to the name of the created entities and increment when each is created.
    private int instanceNumber = 1;

    public BattleActorScriptableObject() : base()
    {
        _equipment  = new();
        _items      = new();
    }

    public BattleActor GenerateBattleActor(bool isPlayerControlled)
    {
        // Creates an instance of the prefab at the current spawn point.
        GameObject currentEntity = Instantiate(spawnablePrefab, FightManager.Instance.PlayerPanel.GetComponent<RectTransform>());

        /// ?????
        BattleActor ba = currentEntity.AddComponent<BattleActor>();
        ba.battleActorValues = this;
        ba.IsPlayerControlled = isPlayerControlled;
        //currentEntity.layer = 5;
        //RectTransform rt = currentEntity.AddComponent<RectTransform>();
        //rt.SetParent(FightManager.Instance.PlayerPanel.GetComponent<RectTransform>());
        //rt.position = new Vector3(-60, 60, 0);
        //rt.sizeDelta = new Vector2(100, 100);
        //currentEntity.AddComponent<Image>().color = new Color(1.0f, 0.25f, 0.25f);
        // this isn't working! Why? :think:

        // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
        currentEntity.name = prefabName + instanceNumber;

        instanceNumber = NextInstanceNumber;

        BattleActor.Instances.Add(currentEntity);
        return ba;
    }

    #region static
    // This will be appended to the name of the created entities and increment when each is created.
    private static int _instanceCounter;
    private static int NextInstanceNumber { get { _instanceCounter++; return _instanceCounter; } }

    public static List<BattleActor> Instances;

    static BattleActorScriptableObject()
    {
        _instanceCounter = 0;
        Instances = new();
    }
    #endregion
}

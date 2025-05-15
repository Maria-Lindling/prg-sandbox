using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BattleActor", order = 1)]
public class BattleActorScriptableObject : ScriptableObject
{
    //start     example code
    public string prefabName;

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

    public BattleActorScriptableObject() : base()
    {
        _equipment  = new();
        _items      = new();
    }


    public BattleActor GenerateBattleActor(bool isPlayerControlled)
    {

        GameObject go = new GameObject("BattleActor");
        go.transform.SetParent(FightManager.Instance.PlayerPanel.transform);
        
        BattleActor ba = go.AddComponent<BattleActor>();
        ba.battleActorValues = this;
        ba.IsPlayerControlled = isPlayerControlled;
        return ba;
    }
}

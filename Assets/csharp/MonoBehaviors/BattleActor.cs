using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleActor : MonoBehaviour
{
    //start     example code

    // The GameObject to instantiate.
    public GameObject entity;

    // An instance of the ScriptableObject defined above.
    public BattleActorScriptableObject battleActorValues;

    // This will be appended to the name of the created entities and increment when each is created.
    private int instanceNumber = 1;

    //end       example code


    public string Name { get => battleActorValues.prefabName; private set => battleActorValues.prefabName = value; }
    public bool IsPlayerControlled { get; set; }

    public bool IsAlive => _isAlive;

    private bool _isAlive = true;

    public BattleActor SpawnEntity(GameObject spawnPoint)
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < battleActorValues.numberOfPrefabsToCreate; i++)
        {
            // Creates an instance of the prefab at the current spawn point.
            GameObject currentEntity = GameObject.Instantiate(entity, spawnPoint.transform);

            /// ?????
            currentEntity.layer = 5;
            currentEntity.AddComponent<RectTransform>();
            currentEntity.GetComponent<RectTransform>().position = new Vector3(-60, 60, 0);
            currentEntity.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            currentEntity.GetComponent<RectTransform>().SetParent(FightManager.Instance.PlayerPanel.transform);
            currentEntity.AddComponent<Image>().color = new Color(1.0f, 0.25f, 0.25f);
            // this isn't working! Why? :think:

            // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
            currentEntity.name = battleActorValues.prefabName + instanceNumber;

            // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % battleActorValues.spawnPoints.Length;

            instanceNumber = NextInstanceNumber;

            Instances.Append(this);
        }

        return this;
    }


    #region static
    // This will be appended to the name of the created entities and increment when each is created.
    private static int _instanceCounter;
    private static int NextInstanceNumber { get { _instanceCounter++; return _instanceCounter; } }

    public static List<BattleActor> Instances;

    static BattleActor()
    {
        _instanceCounter = 0;
        Instances        = new();
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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


    public string Name { get; private set; }
    public bool IsPlayerControlled { get; private set; }


    public BattleActor(string name, bool isPlayerControlled)
    {
        Name = name;
        IsPlayerControlled = isPlayerControlled;

        SpawnEntities();
    }

    private void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < battleActorValues.numberOfPrefabsToCreate; i++)
        {
            // Creates an instance of the prefab at the current spawn point.
            GameObject currentEntity = Instantiate(entity, battleActorValues.spawnPoints[currentSpawnPointIndex], Quaternion.identity);

            // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
            currentEntity.name = battleActorValues.prefabName + instanceNumber;

            // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % battleActorValues.spawnPoints.Length;

            instanceNumber = NextInstanceNumber;

            Instances.Append(this);
        }
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

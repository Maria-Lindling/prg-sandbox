using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance => _instance;



    [SerializeField] private List<Transform> BattleSpawnPoints;
    private List<SpawnPoint> _battleSpawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        if(_instance == null)
        {
            _instance = this;

            _battleSpawnPoints = new();

            BattleSpawnPoints.SelectMany( t => _battleSpawnPoints.Append(new SpawnPoint(t,true)) );
        }
        else
        {
            Destroy( gameObject );
        }
    }

    /// <param name="battleActor"></param>
    /// <param name="spawnPoint">The (first) SpawnPoint that method will
    /// attempt to place the BattleActor at. If it is occupied, the method
    /// will attempt to place the BattleActor the next available one.</param>
    public void SpawnInBattle(IBattleActor<string, int> battleActor, int spawnPoint)
    {

        if (spawnPoint >= _battleSpawnPoints.Count())
            return;

        if(_battleSpawnPoints[spawnPoint].Blocked)
        {
            SpawnInBattle(battleActor, spawnPoint + 1);
        }
        else
        {
            SpawnObject(battleActor.GameObject, _battleSpawnPoints[spawnPoint]);
        }
    }

    public void SpawnObject(GameObject spawnableObject, SpawnPoint spawnPoint)
    {
        Instantiate(spawnableObject,spawnPoint.Use());
    }
}

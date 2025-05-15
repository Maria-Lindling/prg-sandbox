using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance => _instance;


    [SerializeField] private GameObject PlayerSpawnArea;
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
    /// <param name="gameObject"></param>
    public void SpawnInBattle(BattleActor battleActor, GameObject gameObject)
    {
        battleActor.SpawnEntity(gameObject);
    }

    public void SpawnObject(GameObject spawnableObject, SpawnPoint spawnPoint)
    {
        Instantiate(spawnableObject,spawnPoint.Use());
    }
}

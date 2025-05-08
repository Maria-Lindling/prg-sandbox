using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    private static CharacterStatsManager _instance;

    public static CharacterStatsManager Instance { get => _instance; private set => _instance = value; }

    private Dictionary<CharacterStats, ActorStat<int>> _allStats;
    public Dictionary<CharacterStats, ActorStat<int>> All => _allStats;

    private Dictionary<string, bool> _equipment;
    private Dictionary<string, int> _items;


    public int ExperiencePoints {
        get => _allStats[CharacterStats.ExperiencePoints].Value;
        private set => _allStats[CharacterStats.ExperiencePoints].Value = value;
    }
    public int Level {
        get => _allStats[CharacterStats.Level].Value;
        private set => _allStats[CharacterStats.Level].Value = value;
    }
    public int Health {
        get => _allStats[CharacterStats.Health].Value;
        private set => _allStats[CharacterStats.Health].Value = value;
    }
    public int MaxHealth {
        get => _allStats[CharacterStats.Health].Max;
        private set => _allStats[CharacterStats.Health].Max = value;
    }


    public Dictionary<string, bool> Equipment { get => _equipment; private set => _equipment = value; }
    public Dictionary<string, int> Items { get => _items; private set => _items = value; }


    #region custom
    private int _spawnPoint;
    public string SpawnPoint { get => $"SpawnPoint-{_spawnPoint}"; }
    public void SetSpawnPoint(int value) { _spawnPoint = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            _allStats = new();
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

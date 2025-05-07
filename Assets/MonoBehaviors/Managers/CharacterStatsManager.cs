using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour, IStatSheet<string,int>
{
    private static CharacterStatsManager _instance;

    public static CharacterStatsManager Instance { get => _instance; private set => _instance = value; }

    private Dictionary<string, ActorStat<int>> _allStats;
    public Dictionary<string, ActorStat<int>> All => _allStats;


    [SerializeField] private int _experiencePoints;
    [SerializeField] private int _level;
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    private Dictionary<string, bool> _equipment;
    private Dictionary<string, int> _items;


    public int ExperiencePoints { get => _allStats["ExperiencePoints"].Value; private set => _allStats["ExperiencePoints"].Value = value; }
    public int Level { get => _allStats["Level"].Value; private set => _allStats["Level"].Value = value; }
    public int Health { get => _allStats["Health"].Value; private set => _allStats["Health"].Value = value; }
    public int MaxHealth { get => _allStats["Health"].Max; private set => _allStats["Health"].Max = value; }


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

            Load();
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

    private void Load()
    {
        _allStats.Add("ExperiencePoints", new ActorStat<int>(_experiencePoints));

        _allStats.Add("Level", new ActorStat<int>(_level));

        _allStats.Add("Health", new ActorStat<int>(_health));

        _allStats["Health"].Max = _maxHealth;

        Equipment        = new();
        Items            = new();

        _spawnPoint = 0;
    }
}

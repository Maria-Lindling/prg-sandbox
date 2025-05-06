using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    private static CharacterStatsManager _instance;

    public static CharacterStatsManager Instance { get => _instance; private set => _instance = value; }


    [SerializeField] private int _experiencePoints;
    [SerializeField] private int _level;
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Dictionary<string, bool> _equipment;
    [SerializeField] private Dictionary<string, int> _items;

    public int ExperiencePoints { get => _experiencePoints; private set => _experiencePoints = value; }
    public int Level { get => _level; private set => _level = value; }
    public int Health { get => _health; private set => _health = value; }
    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
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
        ExperiencePoints = 0;
        Level            = 1;
        Health           = 100;
        MaxHealth        = 100;

        Equipment        = new();
        Items            = new();

        _spawnPoint = 0;
    }
}

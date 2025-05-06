using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FightManager : MonoBehaviour
{
    private static FightManager _instance;
    public static FightManager Instance { get => _instance; private set => _instance = value; }

    [Range(0, 100), SerializeField] private int _chanceToEncounter;
    public int ChanceToEncounter { get => _chanceToEncounter; set => _chanceToEncounter = value; }


    private DateTime _lastEncounterRoll;
    public DateTime LastEncounterRoll { get => _lastEncounterRoll; private set => _lastEncounterRoll = value; }

    private DateTime _lastEncounterComplete;
    public DateTime LastEncounterComplete { get => _lastEncounterComplete; private set => _lastEncounterComplete = value; }


    private TimeSpan _encounterRollCooldown;
    public TimeSpan EncounterRollCooldown { get => _encounterRollCooldown; set => _encounterRollCooldown = value; }

    private TimeSpan _chainEncounterCooldown;
    public TimeSpan ChainEncounterCooldown { get => _chainEncounterCooldown; set => _chainEncounterCooldown = value; }

    private CombatEncounter _activeEncounter;
    public CombatEncounter ActiveEncounter { get => _activeEncounter; private set => _activeEncounter = value; }

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;

            LastEncounterRoll      = DateTime.Now;

            LastEncounterComplete  = DateTime.MinValue;

            EncounterRollCooldown  = new TimeSpan(0, 0, 0, 0, 700);

            ChainEncounterCooldown = new TimeSpan(0, 0, 0, 2, 100);
        }
        else if(Instance != this)
        {
            Destroy( gameObject );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginNewEncounter(EncounterTables encounterTable = EncounterTables.Default)
    {
        ActiveEncounter = new();
    }

    public bool CheckForEncounter()
    {
        /// There's an additional cooldown that makes sure the player doesn't
        /// instantly enter another encounter.
        if (DateTime.Now - LastEncounterComplete < ChainEncounterCooldown)
            return false;

        /// The game will only roll for an encounter if the cooldown has elapsed
        /// since the last time the game rolled an encounter.
        if (DateTime.Now - LastEncounterRoll < EncounterRollCooldown)
            return false;

        LastEncounterRoll = DateTime.Now;

        if (UnityEngine.Random.Range(0,100) <= ChanceToEncounter)
        {
            Debug.Log("Start Encounter");
            return true;
        }
        else
        {
            Debug.Log("No Encounter");
            return false;
        }

    }
}

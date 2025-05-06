using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    private static FightManager _instance;
    public static FightManager Instance { get => _instance; private set => _instance = value; }

    [Range(0, 100), SerializeField] private int _chanceToEncounter;
    public int ChanceToEncounter { get => _chanceToEncounter; set => _chanceToEncounter = value; }

    private DateTime _lastEncounterRoll;
    private DateTime LastEncounterRoll { get => _lastEncounterRoll; set => _lastEncounterRoll = value; }

    private TimeSpan _encounterRollCooldown;
    private TimeSpan EncounterRollCooldown { get => _encounterRollCooldown; set => _encounterRollCooldown = value; }

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;

            LastEncounterRoll = DateTime.Now;
            EncounterRollCooldown = new TimeSpan(0, 0, 0, 0, 700);
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

    public void CheckForEncounter()
    {
        if (DateTime.Now - LastEncounterRoll < EncounterRollCooldown)
            return;

        LastEncounterRoll = DateTime.Now;

        if (UnityEngine.Random.Range(0,100) <= ChanceToEncounter)
        {
            Debug.Log("Start Encounter");
        }
        else
        {
            Debug.Log("No Encounter");
        }

    }
}

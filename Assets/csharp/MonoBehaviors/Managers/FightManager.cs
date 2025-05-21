using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;

public class FightManager : MonoBehaviour
{
    private static FightManager _instance;
    public static FightManager Instance => _instance;

    [SerializeField] List<BattleActorScriptableObject> allyPool;
    public List<BattleActorScriptableObject> AllyPool => allyPool;


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

    [SerializeField] private GameObject fightCanvas;
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private GameObject enemyPanel;
    public GameObject PlayerPanel => playerPanel;
    public GameObject EnemyPanel => enemyPanel;

    private bool isFightActive => (_activeEncounter != null);

    // Start is called before the first frame update
    void Start()
    {
        if(_instance == null)
        {
            _instance = this;

            LastEncounterRoll      = DateTime.Now;

            LastEncounterComplete  = DateTime.MinValue;

            EncounterRollCooldown  = new TimeSpan(0, 0, 0, 0, 700);

            ChainEncounterCooldown = new TimeSpan(0, 0, 0, 2, 100);
        }
        else if(_instance != this)
        {
            Destroy( gameObject );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckForEncounter(EncounterTables encounterTable = EncounterTables.Default)
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
            StartCoroutine(FightEncounter());
            return true;
        }
        else
        {
            return false;
        }

    }

    private IEnumerator FightEncounter(EncounterTables encounterTable = EncounterTables.Default)
    {
        ActiveEncounter = new();

        // Begin Transition Animation

        //     Load Characters
        ActiveEncounter.LoadCharacters();
        //     Load Random Enemies
        ActiveEncounter.LoadEnemies(encounterTable);
        //     Load BackgroundImages
        //     Load Music
        //     Load UI
        //     Load Items

        // End Transition Animation ; should be a loop with yield return new WaitForEndOfFrame()

        fightCanvas.SetActive(true);

        while (isFightActive)
        {
            // Check whose turn
            // Execute player/ Enemies turn actions
            // Show and wait for end of Fight
            // Set isFightActive to false <- GameOver? Enemies Dead?
            /* yield return new WaitForEndOfFrame();*/
            yield return new WaitForSeconds(60.0f);
            if(true || ActiveEncounter.EndConditionsMet)
            {
                fightCanvas.SetActive(false);
                ActiveEncounter.EndEncounter();
                ActiveEncounter = null;
            }
        }

        // End Fight and gain XP and Gold
        // Level UP?
    }
}

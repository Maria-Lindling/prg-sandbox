using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;

public class BaseCharacterController : MonoBehaviour
{
    private static BaseCharacterController _instance;
    public static BaseCharacterController Instance { get => _instance; private set => _instance = value; }


    [SerializeField] private float movementSpeed;

    private Vector2 movementInput;

    private Rigidbody2D rigidBody;

    private bool isSlowed;

    [Range(0,1)][SerializeField] private float slowedFactor;

    private Tilemap m_tilemap;
    public Tilemap Tilemap
    {
        get
        {
            if (m_tilemap == null) m_tilemap = FindObjectOfType<Tilemap>();
            return m_tilemap;
        }
    }

    public Vector2Int CurrentPosition { get => (Vector2Int) Tilemap.WorldToCell(transform.position); }

    private Vector2Int _lastEncounterPosition;
    public Vector2Int LastEncounterPosition { get => _lastEncounterPosition; set => _lastEncounterPosition = value; }

    private bool _inputLock;
    public bool InputLock { get => _inputLock; set => _inputLock = value; }

    #region custom
    private Vector3 _lastPosition;
    public Vector3 LastPosition { get => _lastPosition; set => _lastPosition = value; }
    #endregion

    private void Start()
    {
        Instance = this;

        rigidBody = GetComponent<Rigidbody2D>();
        isSlowed  = false;
        InputLock = false;

        string spawnPoint = "SpawnPoint-0";

        if(CharacterStatsManager.Instance != null)
        {
            spawnPoint = CharacterStatsManager.Instance.SpawnPoint;
        }

        transform.position = GameObject.Find(spawnPoint).transform.position;

        LastEncounterPosition = CurrentPosition;
        LastPosition          = transform.position;
    }

    // Start is called before the first frame update
    public void Movement(CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (InputLock) return;

        //rigidBody.AddForce((Vector3)movementInput * movementSpeed);

        LastPosition = transform.position;

        transform.Translate(
            new Vector3(movementInput.x, movementInput.y, 0)
                * Time.deltaTime
                * (isSlowed ? movementSpeed * slowedFactor : movementSpeed)
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Swamp")) isSlowed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScreenTransition"))
        {

            (string Destination, int SpawnPoint) = ParseAreaExit(collision.gameObject.name);

            Debug.Log($"!!! Transition to new area: {Destination} !!!");

            CharacterStatsManager.Instance.SetSpawnPoint(SpawnPoint);
            SceneManager.LoadScene(Destination);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch( collision.gameObject.tag )
        {
            case "Swamp": isSlowed = true; break;

            case "EncounterArea":
                if( CheckForEncounter() )
                {
                    LastEncounterPosition = CurrentPosition;
                    FightManager.Instance.BeginNewEncounter(EncounterTables.Default);
                }
                break;

            default: /*Debug.LogError("Unknown trigger area.");*/ break;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Swamp")) isSlowed = false;
    }

    private bool CheckForEncounter()
    {
        /// Will attempt to check for an encounter only if the player has moved to a new tile
        /// and only if the player has made a movement input (exact position has changed)
        if(LastEncounterPosition != CurrentPosition && LastPosition != transform.position)
            return FightManager.Instance.CheckForEncounter();

        return false;
    }


    private (string Destination,int SpawnPoint) ParseAreaExit(string exitName)
    {
        string[] splitExitName = exitName.Split('-');

        return (splitExitName[1], Int32.Parse(splitExitName[2]));
    }
}

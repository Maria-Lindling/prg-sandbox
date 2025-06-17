using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;

public class BaseCharacterController : MonoBehaviour
{
    private static BaseCharacterController _instance;
    public static BaseCharacterController Instance { get => _instance; private set => _instance = value; }


    private GameObject _interactTarget;

    private PlayerInput playerInput;

    public PlayerInput PlayerInput => playerInput;


    [SerializeField] private float movementSpeed;

    private Vector2 movementInput;

    private bool itemMenuToggleInput;

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

    private CharacterAnimationManager characterAnimationManager;

    #region custom
    private Vector3 _lastPosition;
    public Vector3 LastPosition { get => _lastPosition; set => _lastPosition = value; }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;

        rigidBody = GetComponent<Rigidbody2D>();
        isSlowed  = false;
        InputLock = false;

        playerInput = GetComponent<PlayerInput>();

        string spawnPoint = "SpawnPoint-0";

        if(CharacterStatsManager.Instance != null)
        {
            spawnPoint = CharacterStatsManager.Instance.SpawnPoint;
        }

        transform.position = GameObject.Find(spawnPoint).transform.position;

        LastEncounterPosition = CurrentPosition;
        LastPosition          = transform.position;

        characterAnimationManager = GetComponent<CharacterAnimationManager>();
    }

    public void Movement(CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (InputLock)
        {
            characterAnimationManager.SetAnimatorValues(0,0);
            return;
        }

        LastPosition = transform.position;

        transform.Translate(
            new Vector3(movementInput.x, movementInput.y, 0)
                * Time.deltaTime
                * (isSlowed ? movementSpeed * slowedFactor : movementSpeed)
        );

        characterAnimationManager.SetAnimatorValues(movementInput.x, movementInput.y);
    }

    public void PausePlayer(bool isPaused)
    {
        _inputLock = isPaused;

        // Get the specific InputAction for movement.
        InputAction movementAction = playerInput.actions["Movement"];

        if (isPaused)
        {
            // Unsubscribe from the movement input.
            movementAction.performed -= Movement;
            movementAction.canceled -= Movement;
            movementInput = Vector2.zero; // Reset movement input when unpausing
        }
        else
        {
            // Subscribe to the movement input.
            movementAction.performed += Movement;
            movementAction.canceled += Movement;
        }
    }

    #region OnCollisionEnter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision detected with " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Swamp")) isSlowed = true;
    }
    #endregion

    #region OnTriggerEnter/Stay/Exit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ScreenTransition":
                (string Destination, int SpawnPoint) = ParseAreaExit(collision.gameObject.name);

                Debug.Log($"!!! Transition to new area: {Destination} !!!");

                CharacterStatsManager.Instance.SetSpawnPoint(SpawnPoint);
                SceneManager.LoadScene(Destination);
                break;
            
            case "Container":
                _interactTarget = collision.gameObject;
                _interactTarget.GetComponent<ItemContainerManager>().Activate();
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch( collision.gameObject.tag )
        {
            case "Swamp": isSlowed = true; break;

            case "EncounterArea":
                if( CheckForEncounter() ) LastEncounterPosition = CurrentPosition;
                break;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Swamp": isSlowed = false; break;

            case "Container":
                if (_interactTarget == collision.gameObject)
                {
                    collision.gameObject.GetComponent<ItemContainerManager>().Deactivate();
                    _interactTarget = null;
                }
                break;
        }
    }
    #endregion

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

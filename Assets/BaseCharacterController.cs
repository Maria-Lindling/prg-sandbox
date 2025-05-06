using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class BaseCharacterController : MonoBehaviour
{
    private Vector2 movementInput;
    [SerializeField] float movementSpeed;
    private Rigidbody2D rigidBody;

    private bool isSlowed;
    [Range(0,1)][SerializeField] private float slowedFactor;

    private Vector3 lastPosition;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isSlowed = false;

        string spawnPoint = "SpawnPoint-0";

        if(CharacterStatsManager.Instance != null)
        {
            spawnPoint = CharacterStatsManager.Instance.SpawnPoint;
        }

        transform.position = GameObject.Find(spawnPoint).transform.position;

        lastPosition = transform.position;
    }

    // Start is called before the first frame update
    public void Movement(CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //rigidBody.AddForce((Vector3)movementInput * movementSpeed);

        lastPosition = transform.position;

        transform.Translate(
            new Vector3(movementInput.x,movementInput.y,0)
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
            Debug.Log($"!!! Transition to new area: {collision.gameObject.name} !!!");

            (string Destination, int SpawnPoint) = ParseAreaExit(collision.gameObject.name);

            CharacterStatsManager.Instance.SetSpawnPoint(SpawnPoint);
            SceneManager.LoadScene(Destination);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch( collision.gameObject.tag )
        {
            case "Swamp": isSlowed = true; break;

            case "EncounterArea": CheckForEncounter(); break;

            default: /*Debug.LogError("Unknown trigger area.");*/ break;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Swamp")) isSlowed = false;
    }

    private void CheckForEncounter()
    {

        if(lastPosition != transform.position)
            FightManager.Instance.CheckForEncounter();
    }


    private (string Destination,int SpawnPoint) ParseAreaExit(string exitName)
    {
        string[] splitExitName = exitName.Split('-');

        return (splitExitName[1], Int32.Parse(splitExitName[2]));
    }
}

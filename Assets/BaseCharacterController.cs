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

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isSlowed = false;
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
            Debug.Log("!!! Transition to new area. !!!");

            if (SceneManager.GetActiveScene().name == "Game") SceneManager.LoadScene("Town");
            if (SceneManager.GetActiveScene().name == "Town") SceneManager.LoadScene("Game");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Swamp")) isSlowed = true;

        if (collision.gameObject.CompareTag("EncounterArea")) Debug.Log("!!! Roll for random encounter. !!!");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Swamp")) isSlowed = false;
    }
}

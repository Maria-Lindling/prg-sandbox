using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void OnTriggerEnter2D(Collider2D collision)  { }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Swamp")) isSlowed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Swamp")) isSlowed = false;
    }
}

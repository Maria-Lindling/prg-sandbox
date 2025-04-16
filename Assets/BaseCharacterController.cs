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

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    public void Movement(CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidBody.AddForce((Vector3)movementInput * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
    }
}

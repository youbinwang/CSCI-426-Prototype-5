using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }


    private void FixedUpdate()
    {
        if (movementInput.magnitude > 0)
        {
            rb.AddForce(movementInput.normalized * movementSpeed);
        }
    }
}

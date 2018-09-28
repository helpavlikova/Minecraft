using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;

    Rigidbody rb;
    Vector3 moveDirection;
    private bool boxCollision;

    private Vector3 jumpVector = new Vector3(0, 1.5f, 0);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //get direction input from user
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        //calculate direction vector and normalize it so that user would not be walking too quickly
        moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
    }

    void FixedUpdate()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(jumpVector, ForceMode.Impulse);
        Debug.Log("Jump");
    }

    void Move()
    {
        Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0); //in case of falling off a ridge, we need to have some notion of Y direction velocity

        //walk in the given direction at the speed of walkspeed (number of units) per second
        rb.velocity = moveDirection * walkSpeed * Time.deltaTime; //

        rb.velocity += yVelFix; //if we didnt do this, the y velocity would be 0, because it is not set in moveDirection
    }

    void OnTriggerEnter(Collider other)
    {
        boxCollision = true;
        Debug.Log("Player collided with " + other.transform.name);
        other.attachedRigidbody.isKinematic = false;
    }

}

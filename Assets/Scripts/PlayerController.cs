using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;

    Rigidbody rb;
    Vector3 moveDirection;
    public float boxRange = 5;
    private boxScript box;


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
        checkForBoxCollision();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void checkForBoxCollision()
    {
        RaycastHit hit;

        //the raycast ray needs to start a little bit below the eye level of player in order to hit the closest boxes, otherwise the player would aim above them
        Vector3 rayStart = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(rayStart, transform.TransformDirection(Vector3.forward), out hit, boxRange))
        {
            Debug.DrawRay(rayStart, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.collider.gameObject.GetComponent<boxScript>() != null)
            {
                box = hit.collider.gameObject.GetComponent<boxScript>(); //access the box variables
                box.playerIsClose = true;
            }
        }
        else if (box != null)
        {
            box.playerIsClose = false;
            box = null;
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



}

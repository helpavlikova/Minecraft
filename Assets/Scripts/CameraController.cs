﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //player can look only up to 60 degrees up and down, because otherwise they would have to break their necks
    public float maximumX = 60f;
    public float minimumX = -60f;

    //no restriction when looking to the sides
    public float maximumY = 360f;
    public float minimumY = -360f;

    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public float cubeDistance = 5f;

    public Camera cam;
    public GameObject player;

    float rotationY = 0f;
    float rotationX = 0f;

    public Transform buildingCube;

    //offset of the camera
    private Vector3 offset;

    private Vector3 hitPoint;
    private Vector3 direction;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //making the cursor invisible

        offset = Camera.main.transform.position - player.transform.position;

    }

    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * sensitivityY;
        rotationX += Input.GetAxis("Mouse Y") * sensitivityX;

        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

        //rotating the player body
        transform.localEulerAngles = new Vector3(0, rotationY, 0);

        //rotating the player head, e.g. camera
        cam.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);

        if (Input.GetKey(KeyCode.Escape))
        {
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            Application.Quit();
        }

        Camera.main.transform.position = player.transform.position + offset;

        cameraRayTest();

        positionBox();

    }

    void cameraRayTest() //for raycast via camera viewpoint
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
         // print("I'm looking at " + hit.transform.name);

        direction = hit.point - transform.position;
        hitPoint = transform.position + (direction.normalized * cubeDistance);        
    }


    void positionBox()
    {
        buildingCube.position = new Vector3(Mathf.Round(hitPoint.x), Mathf.Round(hitPoint.y), Mathf.Round(hitPoint.z)); //snaps to grid
    }
    
}

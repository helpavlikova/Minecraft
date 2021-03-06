﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour {

    public int hardness;
    public Material highlightMaterial;
    private Material originalMaterial;

    public bool playerIsClose = false;
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule emission;

    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
        emission = ps.emission;
        emission.enabled = false;
        originalMaterial = GetComponent<MeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerIsClose) //triggered by raycast in PlayerController
        {
            GetComponent<MeshRenderer>().material = highlightMaterial; //if player is close enough to destroy a box, it will show as higlighted

        }
        else
        {
            GetComponent<MeshRenderer>().material = originalMaterial;

        }


        if (playerIsClose && Input.GetMouseButton(0))
        {
            hardness--; //destroying the box
            Debug.Log(hardness);
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }

        if (hardness <= 0) {
            Destroy(gameObject);
        }
	}
/*
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerCollider")
        {
            playerIsClose = true;
            GetComponent<MeshRenderer>().material = highlightMaterial;
           // Debug.Log("Close to box");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerCollider")
        {
            playerIsClose = false;
            GetComponent<MeshRenderer>().material = originalMaterial;
           // Debug.Log("No longer in close to box");
        }
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour {

    public int hardness;
    private bool playerIsClose = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerIsClose && Input.GetMouseButton(0))
        {
            hardness--;
            Debug.Log(hardness);
        }

        if (hardness <= 0) {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        playerIsClose = true;
       // Debug.Log("Close to box");
    }

    void OnTriggerExit(Collider other)
    {
        playerIsClose = false;
      //  Debug.Log("No longer in close to box");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the script which operates the box building process
public class OnDrag : MonoBehaviour {
    
    public Rigidbody redBox;
    public Rigidbody greenBox;
    public Rigidbody blueBox;
    public Rigidbody yellowBox;

    public Terrain terrain;
    private TerrainGenerator terrainScript;    

    private bool isFloating = true;
    private bool boxCollision = false;
    private bool buildMode = true;
    private string boxCol;
    private Rigidbody prefab;
   
    void Start()
    {
        prefab = greenBox;
        terrainScript = terrain.GetComponent<TerrainGenerator>(); //access the terraingenerator script
        boxCol = "green";
    }

    void OnTriggerEnter(Collider other)
    {
        boxCollision = true;
       // Debug.Log("Collided with " + other.transform.name);
    }

    void OnTriggerExit(Collider other)
    {
        boxCollision = false;
      //  Debug.Log("No longer in contact with " + other.transform.name);
    }
    

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            buildMode = !buildMode; //toggle buildmode
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled; //toggle visibility of the bulding box
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            prefab = redBox;
            boxCol = "red";
        }
        

        if (Input.GetKeyDown(KeyCode.G))
        {
            prefab = greenBox;
            boxCol = "green";
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            prefab = blueBox;
            boxCol = "blue";
        }


        if (Input.GetKeyDown(KeyCode.Y))
        {
            prefab = yellowBox;
            boxCol = "yellow";
        }

        if (buildMode)
        {
            BuildMode();
        }

    }

    void BuildMode()
    {
        if (transform.position.y == 0f)
            isFloating = false;
        else
            isFloating = true;

        //positionBox();

        //create a new box
        if (Input.GetMouseButtonDown(0) && (!isFloating || boxCollision))
        {
            Rigidbody rigidPrefab;
            rigidPrefab = Instantiate(prefab, transform.position, transform.rotation) as Rigidbody;
            BoxData newBox = new BoxData(transform.position, boxCol);
            terrainScript.customBoxes.Add(newBox);
        }
    }
    
}

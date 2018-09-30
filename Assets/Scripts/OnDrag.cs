using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDrag : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    public Rigidbody redBox;
    public Rigidbody greenBox;
    public Rigidbody blueBox;
    public Rigidbody yellowBox;
    private bool isFloating = true;
    private bool boxCollision = false;
    private bool buildMode = true;
    private enum boxColor {red, green, blue, yellow};
    private Rigidbody prefab;
   
    void Start()
    {
        prefab = greenBox;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
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
        }
        

        if (Input.GetKeyDown(KeyCode.G))
        {
            prefab = greenBox;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            prefab = blueBox;
        }


        if (Input.GetKeyDown(KeyCode.Y))
        {
            prefab = yellowBox;
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

        positionBox();

        //create a new box
        if (Input.GetMouseButtonDown(0) && (!isFloating || boxCollision))
        {
            Rigidbody rigidPrefab;
            rigidPrefab = Instantiate(prefab, transform.position, transform.rotation) as Rigidbody;
        }
    }

    void positionBox()
    {
        //positioning the buildingbox with mouse
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //transform.position = curPosition; classical movement, allows object to be put anywhere
        transform.position = new Vector3(Mathf.Round(curPosition.x), Mathf.Round(curPosition.y), Mathf.Round(curPosition.z)); //snaps to grid
    }
}

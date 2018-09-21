using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDrag : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    public Rigidbody prefab;
    private bool isFloating = true;
    private bool boxCollision = false;
    private bool buildMode = true;
   
    void Start()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnTriggerEnter(Collider other)
    {
        boxCollision = true;
     //   Debug.Log("Collided with " + other.transform.name);
    }

    void OnTriggerExit(Collider other)
    {
        boxCollision = false;
       // Debug.Log("No longer in contact with " + other.transform.name);
    }
    

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B key pressed");
            buildMode = !buildMode; //toggle buildmode
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled; //togle visibility of the bulding box
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

        // Debug.Log("isFloating" + isFloating);
        // Debug.Log("boxCollision" + boxCollision);


        //positioning the buildingbox with mouse
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //transform.position = curPosition; classical movement, allows object to be put anywhere
        transform.position = new Vector3(Mathf.Round(curPosition.x), Mathf.Round(curPosition.y), Mathf.Round(curPosition.z)); //snaps to grid


        //create a new box
        if (Input.GetMouseButtonDown(0) && (!isFloating || boxCollision))
        {
            Rigidbody rigidPrefab;
            rigidPrefab = Instantiate(prefab, transform.position, transform.rotation) as Rigidbody;
        }
    }
}

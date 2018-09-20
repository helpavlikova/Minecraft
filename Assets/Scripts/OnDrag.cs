using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDrag : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    public Rigidbody prefab;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //transform.position = curPosition; classical movement, allows object to be put anywhere
        transform.position = new Vector3(Mathf.Round(curPosition.x), Mathf.Round(curPosition.y), Mathf.Round(curPosition.z)); //snaps to grid
        Debug.Log(transform.position);
    }

    void OnMouseUp()
    {
        Rigidbody rigidPrefab;
        rigidPrefab = Instantiate(prefab, transform.position, transform.rotation) as Rigidbody;

    }

    void Update()
    {
        rb.freezeRotation = true; //so that our cube would not rotate all the time
    }
}

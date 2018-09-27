using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public Rigidbody redBox;
    public Rigidbody greenBox;
    public Rigidbody blueBox;
    public Rigidbody yellowBox;

    private Vector3 boxPosition;

    public int width = 256;
    public int height = 256;
    public int depth = 20; //height on Y axis
    private float boxHeight;

    public float scale = 20F;

    // Use this for initialization
    void Start ()
    {
        Rigidbody rigidPrefab;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                boxHeight = Mathf.Round (calculateHeight(i, j));
                boxPosition = new Vector3(i, boxHeight, j);
               // rigidPrefab = Instantiate(redBox, boxPosition, transform.rotation) as Rigidbody;
                Debug.Log(boxPosition.ToString("F4"));
            }
        }
	}

    float calculateHeight(int i, int j) {
        float result;

        float x = (float) i / width * scale;
        float y = (float) j / height * scale;

        result =  Mathf.PerlinNoise(x, y) * 10;
        Debug.Log(result);

        return result;
    }
}

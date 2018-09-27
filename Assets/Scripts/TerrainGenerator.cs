using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public Rigidbody redBox;
    public Rigidbody greenBox;
    public Rigidbody blueBox;
    public Rigidbody yellowBox;

    private Vector3 boxPosition;

    private int width = 50;
    private int height = 50;
    public int depth = 20; //height on Y axis

    public float scale = 10f;
    public float offsetX;
    public float offsetY;

    // Use this for initialization
    void Start ()
    {

        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float boxHeight = Mathf.Round (calculateHeight(i, j));
                boxPosition = new Vector3(i, boxHeight, j);
                Debug.Log(boxPosition.ToString("F4"));
                generateBox(boxHeight);
            }
        }
	}

    void generateBox(float boxHeight)
    {
        Rigidbody rigidPrefab;

        if (boxHeight < 3)
            rigidPrefab = Instantiate(blueBox, boxPosition, transform.rotation) as Rigidbody;
        else if (boxHeight < 5)
            rigidPrefab = Instantiate(greenBox, boxPosition, transform.rotation) as Rigidbody;
        else if (boxHeight < 7)
            rigidPrefab = Instantiate(redBox, boxPosition, transform.rotation) as Rigidbody;
        else
            rigidPrefab = Instantiate(yellowBox, boxPosition, transform.rotation) as Rigidbody;

    }

    float calculateHeight(int i, int j) {
        float result;

        float x = (float) i / width * scale + offsetX;
        float y = (float) j / height * scale + offsetY;

        result =  Mathf.PerlinNoise(x, y) * 10;
        Debug.Log(result);

        return result;
    }
}

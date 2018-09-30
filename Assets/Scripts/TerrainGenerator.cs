using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainGenerator : MonoBehaviour {

    public Rigidbody redBox;
    public Rigidbody greenBox;
    public Rigidbody blueBox;
    public Rigidbody yellowBox;

    private GameObject[] gameObjects;
    public BoxData[] customBoxes;

    private Vector3 boxPosition;

    private int width = 50;
    private int height = 50;
    public int depth = 20; //height on Y axis

    public float scale = 10f;
    public float offsetX;
    public float offsetY;

    public int beginX = 0;
    public int beginY = 0;

    // Use this for initialization
    void Start ()
    {

        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);

        Debug.Log("generated OffsetX " + offsetX);
        Debug.Log("generated OffsetY " + offsetY);

        generateTerrain();        
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Save();
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    void generateTerrain()
    {
        for (int i = beginX; i < width; i++)
        {
            for (int j = beginY; j < height; j++)
            {
                float boxHeight = Mathf.Round(calculateHeight(i, j));
                boxPosition = new Vector3(i, boxHeight, j);
                // Debug.Log(boxPosition.ToString("F4"));
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
       // Debug.Log(result);

        return result;
    }

    public void Save()
    {
        SaveLoad.Save(this);
    }

    public void Load()
    {
        WorldData loadedData = SaveLoad.Load();

        float[] loadedOffsets = loadedData.offsets;
        int[] loadedStartPoints = loadedData.beginCoords;

        offsetX = loadedOffsets[0];
        offsetY = loadedOffsets[1];


        DestroyEnvironment();
        generateTerrain();

        Debug.Log("loaded OffsetX " + offsetX);
        Debug.Log("loaded OffsetY " + offsetY);

        Debug.Log("loaded startpoint X " + loadedStartPoints[0]);
        Debug.Log("loaded startpoint Y " + loadedStartPoints[1]);
    }

    void DestroyEnvironment()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("environment");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}

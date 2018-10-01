using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainGenerator : MonoBehaviour {

    //prefab boxes
    public Rigidbody redBox;
    public Rigidbody greenBox;
    public Rigidbody blueBox;
    public Rigidbody yellowBox;

    private GameObject[] gameObjects; //to handle all objects in scene for deleting purposes 
    public List<BoxData> customBoxes; //to store user made boxes

    private Vector3 boxPosition;

    //world size
    private int width = 50;
    private int height = 50;
    public int depth = 20; //height on Y axis

    //perlin noise variables
    public float scale = 10f;
    public float offsetX;
    public float offsetY;
    public int beginX = 0;
    public int beginY = 0;
    
    void Start ()
    {
        //randomization of map generated
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);

        Debug.Log("generated OffsetX " + offsetX);
        Debug.Log("generated OffsetY " + offsetY);

        generateTerrain();
        customBoxes = new List<BoxData>();        
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

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Deleting scene for testing purposes");
            DestroyEnvironment();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            recreateCustomBoxes();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (customBoxes.Count != 0)
            {
                foreach (var box in customBoxes)
                {
                    box.printBox();
                }
            }
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
        customBoxes = loadedData.boxes;

        offsetX = loadedOffsets[0];
        offsetY = loadedOffsets[1];


        DestroyEnvironment();
        generateTerrain();
        recreateCustomBoxes();

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

    void recreateCustomBoxes()
    {

        if (customBoxes.Count != 0)
        {
            foreach (var box in customBoxes)
            {
                Vector3 pos = new Vector3(box.positionX, box.positionY, box.positionZ);
                Rigidbody rigidPrefab;

                switch (box.color)
                {
                    case "red":
                        rigidPrefab = Instantiate(redBox, pos, transform.rotation) as Rigidbody;
                        break;
                    case "green":
                        rigidPrefab = Instantiate(greenBox, pos, transform.rotation) as Rigidbody;
                        break;
                    case "blue":
                        rigidPrefab = Instantiate(blueBox, pos, transform.rotation) as Rigidbody;
                        break;
                    case "yellow":
                        rigidPrefab = Instantiate(yellowBox, pos, transform.rotation) as Rigidbody;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

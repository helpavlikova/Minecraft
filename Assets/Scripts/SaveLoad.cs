using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;


public static class SaveLoad {


    public static void Save(TerrainGenerator terrainData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.sv");
        WorldData data = new WorldData(terrainData);
        bf.Serialize(file, data);
        file.Close();
    }

    public static WorldData Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.sv"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.sv", FileMode.Open);
            WorldData data = bf.Deserialize(file) as WorldData;

            file.Close();
            return data;
        }
        else
        {
            Debug.Log("File does not exist yet");
            return null;
        }

    }

}

[Serializable]
public class WorldData {
    public float[] offsets; //offsets to generate terrain via perlin noise
    public int[] beginCoords;

    public BoxData[] boxes;

    public WorldData(TerrainGenerator terrainData)
    {
        offsets = new float[2];
        offsets[0] = terrainData.offsetX;
        offsets[1] = terrainData.offsetY;

        beginCoords = new int[2];
        beginCoords[0] = terrainData.beginX;
        beginCoords[1] = terrainData.beginY;
    }
}

public class BoxData
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public string color;

    public BoxData(Vector3 position, string newColor)
    {
        positionX = position.x;
        positionY = position.y;
        positionZ = position.z;
        color = newColor;
    }

    public void printBox()
    {
        Debug.Log(color + " box at position " + positionX + " " + positionY + " " + positionZ);
    }
}

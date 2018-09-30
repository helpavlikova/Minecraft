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

    public static float[] Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.sv"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.sv", FileMode.Open);
            WorldData data = bf.Deserialize(file) as WorldData;

            file.Close();
            return data.offsets;
        }
        else
        {
            Debug.Log("File does not exist yet");
            return new float[2];
        }

    }

}

[Serializable]
public class WorldData {
    public float[] offsets;

    public WorldData(TerrainGenerator terrainData)
    {
        offsets = new float[2];
        offsets[0] = terrainData.offsetX;
        offsets[1] = terrainData.offsetY;
    }
}

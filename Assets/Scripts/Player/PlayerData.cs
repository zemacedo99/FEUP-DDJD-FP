using System.Collections;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UIElements;

public static class PlayerSaveSystem
{
    public static string savePath = "/playerData.Save";

    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create);

        PlayerData playerData = new PlayerData();

        playerData.Load();

        formatter.Serialize(stream, playerData);

        stream.Close();
    }


    public static PlayerData Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerData;
        }
        else
        {
            Debug.LogError("Save File not found in " + string.Concat(Application.persistentDataPath, savePath));
            return null;
        }
    }

    public static void ResetData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create);

        PlayerData playerData = new PlayerData();

        formatter.Serialize(stream, playerData);

        stream.Close();
    }

}

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public float oxygenLevel;
    public int completedPuzzles;

    public PlayerData()
    {
        position = new float[3];

        position[0] = 0f;
        position[1] = 0f;
        position[2] = 0f;

        oxygenLevel = 300;

        completedPuzzles = 0;
    }

    public void Load()
    {
        position = new float[3];

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<Oxygen>() != null)
        {
            oxygenLevel = player.GetComponent<Oxygen>().oxygenValue;
        }
        else
        {
            oxygenLevel = 300;
        }

        position[0] = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        position[1] = GameObject.FindGameObjectWithTag("Player").transform.position.y;
        position[2] = GameObject.FindGameObjectWithTag("Player").transform.position.z;
    }
} 


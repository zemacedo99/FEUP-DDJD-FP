/**using System.Collections;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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
    public float[] positionWorld;
    public float oxygenLevel;
    public int completedPuzzles;
    public string currentScene;
    public float[] positionPuzzle;

    public PlayerData()
    {
        positionWorld = new float[3];
        positionPuzzle = new float[3];

        positionWorld[0] = 0f;
        positionWorld[1] = 1.50f;
        positionWorld[2] = 0f;

        positionPuzzle[0] = 0f;
        positionPuzzle[1] = 0f;
        positionPuzzle[2] = 0f;

        oxygenLevel = 300;

        currentScene = SceneManager.GetActiveScene().name;

        completedPuzzles = 0;
    }

    public void Load()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<Oxygen>() != null)
        {
            oxygenLevel = player.GetComponent<Oxygen>().oxygenValue;
        }

        if (currentScene != "World")
        {
            positionPuzzle[0] = GameObject.FindGameObjectWithTag("Player").transform.position.x;
            positionPuzzle[1] = GameObject.FindGameObjectWithTag("Player").transform.position.y;
            positionPuzzle[2] = GameObject.FindGameObjectWithTag("Player").transform.position.z;
        }

        positionWorld[0] = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        positionWorld[1] = GameObject.FindGameObjectWithTag("Player").transform.position.y;
        positionWorld[2] = GameObject.FindGameObjectWithTag("Player").transform.position.z;
    }
} 

**/
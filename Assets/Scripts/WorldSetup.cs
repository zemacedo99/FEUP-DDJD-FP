using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

public class WorldSetup : SceneDict
{
    void Start()
    {
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            var name = NameFromPath(scene.path);
            if (name == "World")
                continue;
            if (!PlayerPrefs.HasKey(name) || true) //check if scene hasnt been set yet
                PlayerPrefs.SetInt(name, 0); //set state 0 (dropped)
            int state = PlayerPrefs.GetInt(name);
            var water = GameObject.Find(holeDict[name]).GetComponent<RaiseWater>(); //get associated water script
            if (state == 0)
                water.Drop(false); //drop and dont raise
            else if (state == 1) {
                water.Drop(true); //drop but raise
                GameObject.Find(holeDict[name]).GetComponentInChildren<LevelTrigger>().gameObject.SetActive(false);
                PlayerPrefs.SetInt(name, 2); //and set state to raised
            }
        }
    }

    string NameFromPath(string path)
    {
        int header = "Assets/Scenes/".Length;
        int drop = ".unity".Length;
        return path.Substring(header, path.Length - header - drop);
    }
}

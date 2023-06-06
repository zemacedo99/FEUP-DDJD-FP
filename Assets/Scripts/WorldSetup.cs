using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

public class WorldSetup : SceneDict
{
    public FMODUnity.EventReference oxygenStationEmptyEvent;
    private FMOD.Studio.EventInstance oxygenStationEmptyEventInstance;
    List<FMOD.Studio.EventInstance> oxygenStationEmptyEventInstanceArray = new List<FMOD.Studio.EventInstance>();

    void Start()
    {
        bool resetToPoint = true;
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            var name = NameFromPath(scene.path);
            if (name == "World" || name == "MainMenu")
                continue;
            if (!PlayerPrefs.HasKey(name)) //check if scene hasnt been set yet
                PlayerPrefs.SetInt(name, 0); //set state 0 (dropped)
            int state = PlayerPrefs.GetInt(name);
            GameObject hole = GameObject.Find(holeDict[name]);
            RaiseWater water = hole.GetComponent<RaiseWater>(); //get associated water script
            if (state == 0)
            {
                water.Drop(false); //drop and dont raise

                // Play Empty Sound
                //FMODUnity.RuntimeManager.PlayOneShotAttached(oxygenStationEmptyEvent, hole);
                oxygenStationEmptyEventInstance = FMODUnity.RuntimeManager.CreateInstance(oxygenStationEmptyEvent);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(oxygenStationEmptyEventInstance, hole.transform);
                oxygenStationEmptyEventInstance.start();

                oxygenStationEmptyEventInstanceArray.Add(oxygenStationEmptyEventInstance);
            }
            else if (state == 1) {
                water.Drop(true); //drop but raise
                GameObject.Find(holeDict[name]).GetComponentInChildren<LevelTrigger>().gameObject.SetActive(false);
                PlayerPrefs.SetInt(name, 2); //and set state to raised
                resetToPoint = false;
            }
        }
        if (resetToPoint)
        {
            print("loading checkpoint");
            FindFirstObjectByType<Oxygen>().LoadCheckpoint();

        }
    }

    string NameFromPath(string path)
    {
        int header = "Assets/Scenes/".Length;
        int drop = ".unity".Length;
        return path.Substring(header, path.Length - header - drop);
    }

    private void OnDestroy()
    {
        foreach (var instance in oxygenStationEmptyEventInstanceArray)
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

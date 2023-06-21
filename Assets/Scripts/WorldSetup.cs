using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSetup : SceneDict
{
    public FMODUnity.EventReference oxygenStationEmptyEvent;
    private FMOD.Studio.EventInstance oxygenStationEmptyEventInstance;
    List<FMOD.Studio.EventInstance> oxygenStationEmptyEventInstanceArray = new List<FMOD.Studio.EventInstance>();

    void Start()
    {
        var oxy = FindAnyObjectByType<Oxygen>();
        var player = oxy.gameObject;
        var playerSpawn = player.transform.position;
        var playerRot = 0f;

        if (oxy.HasCheckpoint())
        {
            playerSpawn = oxy.GetCheckpoint();
            playerRot = oxy.GetRot();
        }


        string[] sceneNames = { "Puzzle", "Puzzle2", "Puzzle3", "Puzzle4", "Puzzle5" };
        foreach (var name in sceneNames)
        {
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
                playerSpawn = water.Drop(true); //drop but raise
                playerRot = water.y_Rot;
                GameObject.Find(holeDict[name]).GetComponentInChildren<LevelTrigger>().gameObject.SetActive(false);
                PlayerPrefs.SetInt(name, 2); //and set state to raised
            }
        }

        player.transform.position = playerSpawn;
        player.transform.rotation = Quaternion.Euler(0,playerRot,0);
    }

    private void OnDestroy()
    {
        foreach (var instance in oxygenStationEmptyEventInstanceArray)
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

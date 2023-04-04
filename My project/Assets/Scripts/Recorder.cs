using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public enum ActionType { Move, Jump, StopRecording };

    public bool isRecording;
    bool isPlaying;
    Vector3 startingPosition;
    float recordingStartTime;
    float playStartTime;

    GameObject clone;
    CharacterController cloneController;
    Cloning cloningScript;
    List<Tuple<ActionType, float, Vector3>> actionsArray;

    // Start is called before the first frame update
    void Start()
    {
        isRecording = false;
        actionsArray = new List<Tuple<ActionType, float, Vector3>>();

        cloningScript = GetComponent<Cloning>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            foreach (Tuple<ActionType, float, Vector3> tuple in actionsArray)
            {
                if (tuple.Item2 - recordingStartTime <= Time.time - playStartTime) {
                    switch (tuple.Item1)
                    {
                        case ActionType.Move:
                            //cloneController.Move(tuple.Item3);
                            break;
                        case ActionType.Jump:
                            PlayerMovement pm = clone.GetComponent<PlayerMovement>();
                            pm.Jump(pm.jumpHeight, pm.gravity);
                            break;
                        case ActionType.StopRecording:
                            isPlaying = false;
                            Destroy(gameObject);
                            break;
                    }
                    actionsArray.RemoveAt(0);
                }
            }
        }   
    }

    public void StartRecording()
    {
        Debug.Log("Recording Started");

        isRecording = true;
        recordingStartTime = Time.time;

        // Store starting position, and facing direction (XZ only) and gravity modifier (1 or -1)
        startingPosition = gameObject.transform.position;

        // Reset actionsArray
        actionsArray = new List<Tuple<ActionType, float, Vector3>>();
    }

    public void StopRecording()
    {
        if (!isRecording) return;
        Debug.Log("Recording Stopped");

        isRecording = false;

        Push(ActionType.StopRecording, Time.time);

        // Destroy Player and instantiate them again at the starting position
        GameObject player = Instantiate(gameObject, startingPosition, Quaternion.identity);
        player.GetComponent<Cloning>().isClone = false;
        Destroy(gameObject);

        Debug.Log(actionsArray.Count);
    }

    public void Play()
    {
        Debug.Log("Size of array: " + actionsArray.Count);

        isPlaying = true;
        StopRecording();

        playStartTime = Time.time;

        clone = Instantiate(gameObject, startingPosition, Quaternion.identity);
        //clone.GetComponent<Cloning>().InitClone();
        cloneController = GetComponent<CharacterController>();
    }

    public void Push(ActionType actionType, float timestamp)
    {
        actionsArray.Add(new Tuple<ActionType, float, Vector3>(actionType, timestamp, new Vector3(0,0,0)) );
    }
    public void Push(ActionType actionType, float timestamp, Vector3 motion)
    {
        actionsArray.Add(new Tuple<ActionType, float, Vector3>(actionType, timestamp, motion));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public enum EventType { MoveDirUpdate, CameraEvent, Jump, StopRecording };

    public bool isRecording;
    public bool isPlaying;
    Vector3 startingPosition;
    GameObject startingCamera;
    float recordingStartTime;
    float playStartTime;

    Cloning cloningScript;
    GameObject clone;
    public GameObject cube;

    // Clone Playing
    CharacterController cloneController;
    int playIndex;
    List<Tuple<EventType, float, Vector3>> eventArray;


    // Start is called before the first frame update
    void Start()
    {
        isRecording = false;
        eventArray = new List<Tuple<EventType, float, Vector3>>();

        cloningScript = GetComponent<Cloning>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying && !cloningScript.isClone)
        {
            Tuple<EventType, float, Vector3> tuple = GetNextEvent();

            while (tuple != null && tuple.Item2 <= Time.time - playStartTime)
            {
                PlayerMovement pm = clone.GetComponent<PlayerMovement>();
                switch (tuple.Item1)
                {
                    //case EventType.MoveDirUpdate:
                    //    if (tuple.Item3 != moveDirVector)
                    //        moveDirVector = tuple.Item3;
                    //    cloneController.Move(moveDirVector);
                    //    break;
                    case EventType.CameraEvent:
                        pm.transform.Rotate(tuple.Item3);
                        break;
                    //case EventType.Jump:
                    //    pm.SetVelocityY(pm.Jump(pm.jumpHeight, pm.gravity));
                    //    Debug.Log("Jumping");
                    //    break;
                    case EventType.StopRecording:
                        isPlaying = false;
                        Destroy(clone);
                        break;
                }
                if (IncrementEventIndex())
                    tuple = GetNextEvent();
                else break;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1) && !cloningScript.isClone)
            {
                StartRecording();
            }
            else if (Input.GetMouseButtonUp(1) && !cloningScript.isClone)
            {
                StopRecording();
            }

            if (Input.GetKeyDown(KeyCode.Q) && !cloningScript.isClone)
            {
                Play();
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

        // Find startingCamera
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.layer == LayerMask.NameToLayer("Cameras"))
            {
                if (child.name == "Starting Camera")
                {
                    Destroy(child);
                }
                else {
                    // child is startingCamera
                    var oldStartingCamera = startingCamera;
                    Destroy(oldStartingCamera);

                    startingCamera = Instantiate(child, gameObject.transform);
                    startingCamera.name = "Starting Camera";
                    startingCamera.tag = "Untagged";
                    startingCamera.GetComponent<Camera>().enabled = false;
                    startingCamera.transform.parent = cube.transform;
                }
            }
        }
        // More ToDo

        // Reset eventArray
        eventArray = new List<Tuple<EventType, float, Vector3>>();
    }

    public void StopRecording()
    {
        if (!isRecording) return;
        Debug.Log("Recording Stopped");

        isRecording = false;

        Push(EventType.StopRecording, Time.time - recordingStartTime);

        // Destroy Player and instantiate them again at the starting position
        //GameObject player = Instantiate(gameObject, startingPosition, Quaternion.identity);
        //player.GetComponent<Cloning>().isClone = false;
        //Destroy(gameObject);

        Debug.Log(eventArray.Count);
    }

    public void Play()
    {
        if (isRecording) return;
        if (isPlaying)
        {
            Destroy(clone);
        }

        Debug.Log("Size of array: " + eventArray.Count);
        if (eventArray.Count < 1)
        {
            Debug.Log("No events to play");
            return;
        }

        playIndex = 0;
        ResetAllPlayIndexes();

        playStartTime = Time.time;

        clone = Instantiate(gameObject, startingPosition, Quaternion.identity);
        clone.GetComponent<Cloning>().InitClone(startingCamera);
        cloneController = clone.GetComponent<CharacterController>();

        isPlaying = true;
    }

    public void Push(EventType eventType, float timestamp)
    {
        eventArray.Add(new Tuple<EventType, float, Vector3>(eventType, timestamp, new Vector3(0,0,0)) );
    }
    public void Push(EventType eventType, float timestamp, Vector3 motion)
    {
        eventArray.Add(new Tuple<EventType, float, Vector3>(eventType, timestamp, motion));
    }

    public float GetRecordingStartTime()
    {
        return recordingStartTime;
    }
    public float GetPlayStartTime()
    {
        return playStartTime;
    }

    public Tuple<EventType, float, Vector3> GetNextEvent()
    {
        return playIndex < eventArray.Count ? eventArray[playIndex] : null;
    }
    public Tuple<EventType, float, Vector3> GetEvent(int index)
    {
        return index < eventArray.Count ? eventArray[index] : null;
    }
    public bool IncrementEventIndex()
    {
        playIndex++;
        return true && playIndex < eventArray.Count;
    }

    // Resets Play Indexes in ALL scripts
    void ResetAllPlayIndexes()
    {
        gameObject.GetComponent<PlayerMovement>().ResetPlayIndexes();
    }
}

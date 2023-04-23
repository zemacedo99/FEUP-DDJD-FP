using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public enum EventType { MoveDirUpdate, CameraInputValueUpdate, Jump, StopRecording };

    public bool isRecording;
    public bool isPlaying;
    Vector3 startingPosition;
    Quaternion startingRotation;
    public GameObject startingCamera;
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
            // Get Playback Value
            Tuple<EventType, float, Vector3> tuple = GetNextEvent();
            if (tuple != null && tuple.Item2 <= Time.time - playStartTime)
            {
                if (tuple.Item1 == EventType.StopRecording)
                {
                    isPlaying = false;
                    Destroy(clone);
                }
                playIndex++;
                //tuple = GetEvent(playIndex);
            }
        }
        
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

    public void StartRecording()
    {
        if (isRecording) return;
        Debug.Log("Recording Started");

        isRecording = true;
        recordingStartTime = Time.time;

        // Store starting position, starting camera and ToDo: gravity modifier (1 or -1)
        startingPosition = gameObject.transform.position;
        startingRotation = gameObject.transform.rotation;

        // Re-Instantiate startingCamera
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.CompareTag("MainCamera"))
            {
                // child is MainCamera
                // Destroy old startingCamera
                var oldStartingCamera = startingCamera;
                Destroy(oldStartingCamera);

                // Create new startingCamera
                startingCamera = Instantiate(child, child.transform.localPosition, child.transform.localRotation);
                startingCamera.transform.SetParent(cube.transform, false);
                Debug.Log(child.transform.localRotation);
                Debug.Log(startingCamera.transform.localRotation);

                startingCamera.name = "Starting Camera";
                startingCamera.tag = "Untagged";
                startingCamera.GetComponent<Camera>().enabled = false;

                // Update cube position
                cube.transform.position = startingPosition;
            }
        }

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
        //GameObject player = Instantiate(gameObject, startingTransform);
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

        clone = Instantiate(gameObject, startingPosition, startingRotation);
        clone.GetComponent<Cloning>().InitClone();
        cloneController = clone.GetComponent<CharacterController>();
        Debug.Log(clone.GetComponent<PlayerMovement>().playerCamera.transform.localRotation);

        playIndex = 0;
        ResetAllPlayIndexes();
        playStartTime = Time.time;
        isPlaying = true;
    }

    // Pushes an event to the eventArray.
    // timestamp is time elapsed from recordingStartTime.
    public void Push(EventType eventType, float timestamp)
    {
        eventArray.Add(new Tuple<EventType, float, Vector3>(eventType, timestamp, new Vector3(0,0,0)) );
    }
    public void Push(EventType eventType, float timestamp, Vector3 motion)
    {
        eventArray.Add(new Tuple<EventType, float, Vector3>(eventType, timestamp, motion));
    }

    // Getters
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
    public Vector3 GetStartingPosition()
    {
        return startingPosition;
    }
    public Quaternion GetStartingRotation()
    {
        return startingRotation;
    }

    // Resets Play Indexes in ALL scripts
    void ResetAllPlayIndexes()
    {
        gameObject.GetComponent<PlayerMovement>().ResetPlayIndexes();
    }
}

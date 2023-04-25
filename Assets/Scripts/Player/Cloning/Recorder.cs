using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAction
{
    public Vector3 position;
    public Quaternion rotation;
    public Quaternion cameraRotation; // debug
    public float delay;

    public PlayerAction(Vector3 position, Quaternion rotation, Quaternion cameraRotation, float delay)
    {
        this.position = position;
        this.rotation = rotation;
        this.cameraRotation = cameraRotation;
        this.delay = delay;
    }
}


public class Recorder : MonoBehaviour
{
    public enum EventType { MoveInputValueUpdate, CameraInputValueUpdate, Jump, StopRecording };

    public bool isRecording;
    public bool isPlaying;
    Vector3 startingPosition;
    Quaternion startingRotation;
    public GameObject startingCamera;
    float lastFrame;

    Cloning cloningScript;
    public GameObject cube;


    public List<PlayerAction> eventArray;


    // Start is called before the first frame update
    void Start()
    {
        isRecording = false;
        eventArray = new List<PlayerAction>();

        cloningScript = GetComponent<Cloning>();
    }

    void FixedUpdate()
    {
        if (isRecording)
            eventArray.Add(new PlayerAction(transform.position, transform.rotation, startingCamera.transform.localRotation, Time.deltaTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Record
            if (!isRecording)
            {
                Debug.Log("Recording Started");

                eventArray.Clear();
                isRecording = true;
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            // Stop Recording
            if (isRecording)
            {
                Debug.Log("Recording Stopped");
                Debug.Log("Recording actions: " + eventArray.Count);
                isRecording = false;
            }
        }
    }

}

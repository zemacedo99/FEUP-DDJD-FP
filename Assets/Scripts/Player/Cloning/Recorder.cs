using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerSnapshot
{
    public Vector3 position;
    public Quaternion rotation;
    public Quaternion cameraRotation; // debug
    public float timestamp;

    public PlayerSnapshot(Vector3 position, Quaternion rotation, Quaternion cameraRotation, float timestamp)
    {
        this.position = position;
        this.rotation = rotation;
        this.cameraRotation = cameraRotation;
        this.timestamp = timestamp;
    }
}
public class PlayerEvent
{
    public enum EventType { MoveInputValueUpdate, CameraInputValueUpdate, Jump, StopRecording };
    public float timestamp;
}

public class Recorder : MonoBehaviour
{
    public bool isRecording;
    public bool isPlaying;
    public GameObject startingCamera;
    float recordingStartTime;

    Cloning cloningScript;
    public GameObject cube;

    public List<PlayerSnapshot> snapshotArray;

    public InputActionAsset actions;
    public InputAction recordButton, playButton;

    // Start is called before the first frame update
    void Start()
    {
        recordButton = actions.FindActionMap("recorder", true).FindAction("record", true);
        playButton = actions.FindActionMap("recorder", true).FindAction("play", true);
        actions.FindActionMap("recorder").Enable();

        isRecording = false;
        snapshotArray = new List<PlayerSnapshot>();

        cloningScript = GetComponent<Cloning>();
    }

    void FixedUpdate()
    {
        if (isRecording)
            snapshotArray.Add(new PlayerSnapshot(transform.position, transform.rotation, startingCamera.transform.localRotation, Time.time - recordingStartTime));
    }

    // Update is called once per frame
    void Update()
    {
        if ((recordButton.WasPressedThisFrame()))
        {
            // Record
            if (!isRecording)
            {
                Debug.Log("Recording Started");

                snapshotArray.Clear();
                recordingStartTime = Time.time;
                isRecording = true;

                // Instantiate cube here
            }
        }
        else if ((recordButton.WasReleasedThisFrame()))
        {
            // Stop Recording
            if (isRecording)
            {
                snapshotArray.Add(new PlayerSnapshot(transform.position, transform.rotation, startingCamera.transform.localRotation, Time.time - recordingStartTime));

                isRecording = false;
                Debug.Log("Recording Stopped");
                Debug.Log("Recording snapshots: " + snapshotArray.Count);
                Debug.Log("Recording duration: " + (Time.time - recordingStartTime));
            }
        }
    }

    public float GetRecordingStartTime()
    {
        return recordingStartTime;
    }
}

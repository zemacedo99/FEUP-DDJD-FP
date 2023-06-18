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
    public enum EventType { Jump, FootstepsSound };
    public EventType type;
    public float timestamp;

    public PlayerEvent(EventType eventType, float timestamp)
    {
        this.type = eventType;
        this.timestamp = timestamp;
    }
}

public class Recorder : MonoBehaviour
{
    public bool isRecording;
    public bool isPlaying;
    public GameObject playerCamera;
    PlayerMovement playerMovement;

    float recordingStartTime;

    public List<PlayerSnapshot> snapshotArray;
    public List<PlayerEvent> eventArray;
    public GameObject cube;
    GameObject newCube;
    List<GameObject> cubesStack;
    public int cubesStackLimit = 0;
    public ItemObject cube1;
    public ItemObject cube2;

    public InputActionAsset actions;
    public InputAction recordButton;
    public InputAction cubePopButton;

    CanvasScript canvasScript;

    public FMODUnity.EventReference recordingEvent;
    FMOD.Studio.EventInstance recordingEventInstance;
    public FMODUnity.EventReference cubePopEvent;

    // Start is called before the first frame update
    void Start()
    {
        recordButton = actions.FindActionMap("recorder", true).FindAction("record", true);
        cubePopButton = actions.FindActionMap("recorder", true).FindAction("cubePop", true);
        actions.FindActionMap("recorder").Enable();

        isRecording = false;
        snapshotArray = new List<PlayerSnapshot>();
        eventArray = new List<PlayerEvent>();

        playerMovement = gameObject.GetComponent<PlayerMovement>();
        canvasScript = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<CanvasScript>();

        cubesStack = new List<GameObject>();

        cubesStackLimit = 0;

    }

    void FixedUpdate()
    {
        if (isRecording)
            snapshotArray.Add(new PlayerSnapshot(transform.position, transform.rotation, playerCamera.transform.localRotation, Time.time - recordingStartTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasScript.isPaused) return;

        if (recordButton.WasPressedThisFrame())
        {
            // Record
            if (!isRecording && cubesStack.Count < cubesStackLimit)
            {
                Debug.Log("Recording Started");

                snapshotArray.Clear();
                eventArray.Clear();
                recordingStartTime = Time.time;
                isRecording = true;

                // Instantiate cube
                newCube = Instantiate(cube, transform.position - Vector3.up * 0.3f, transform.rotation);
                cubesStack.Add(newCube);

                recordingEventInstance = FMODUnity.RuntimeManager.CreateInstance(recordingEvent);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(recordingEventInstance, transform);
                recordingEventInstance.start();
            }
        }
        else if (recordButton.WasReleasedThisFrame())
        {
            // Stop Recording
            if (isRecording)
            {
                snapshotArray.Add(new PlayerSnapshot(transform.position, transform.rotation, playerCamera.transform.localRotation, Time.time - recordingStartTime));

                isRecording = false;
                Debug.Log("Recording Stopped");
                Debug.Log("Recorded snapshots: " + snapshotArray.Count);
                Debug.Log("Recorded events: " + eventArray.Count);
                Debug.Log("Recording duration: " + (Time.time - recordingStartTime));

                newCube.GetComponent<Cloning>().SetSnapshotArray(snapshotArray);
                newCube.GetComponent<Cloning>().SetEventArray(eventArray);

                recordingEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }

        if (cubePopButton.WasPressedThisFrame())
        {
            if (cubesStack.Count != 0)
            {
                Destroy(cubesStack[0]);
                FMODUnity.RuntimeManager.PlayOneShotAttached(cubePopEvent, cubesStack[0]);
                cubesStack.RemoveAt(0);

            }
        }
    }

    public float GetRecordingStartTime()
    {
        return recordingStartTime;
    }

    private void OnDestroy()
    {
        recordingEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void UpdateCubeStackLimit()
    {
        cubesStackLimit = 0;
        if (GetComponent<PlayerInventory>().HasItem(cube1))
            cubesStackLimit++;
        if (GetComponent<PlayerInventory>().HasItem(cube2))
            cubesStackLimit++;
    }
}

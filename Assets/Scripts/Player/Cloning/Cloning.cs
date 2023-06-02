using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cloning : MonoBehaviour
{
    public Recorder recorder;
    
    public GameObject clonePrefab;
    private float playTimer;
    private float playCooldown = 1f;

    public InputActionAsset actions;
    public InputAction playButton;

    List<PlayerSnapshot> snapshotArray;
    List<PlayerEvent> eventArray;

    CanvasScript canvasScript;

    private void Start()
    {
        playButton = actions.FindActionMap("recorder", true).FindAction("play", true);
        actions.FindActionMap("recorder").Enable();

        recorder = GameObject.FindGameObjectWithTag("Player").GetComponent<Recorder>();
        canvasScript = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<CanvasScript>();

        playTimer = playCooldown;
    }

    void Update()
    {
        if (canvasScript.isPaused) return;

        if (playButton.WasPressedThisFrame() && !recorder.isRecording && playTimer >= playCooldown)
        {
            if (recorder.snapshotArray.Count > 0)
                SpawnClone();
            else
            {
                Debug.Log("No snapshots to play");
            }

            // Reset playTimer
            playTimer = 0f;
        }

        if (playTimer < playCooldown) playTimer += Time.deltaTime;
    }

    void SpawnClone()
    {
        // Get first position and rotation
        Vector3 initialPosition = snapshotArray[0].position;
        Quaternion initialRotation = snapshotArray[0].rotation;

        GameObject clone = Instantiate(clonePrefab, initialPosition, initialRotation);

        Clone cloneScript = clone.GetComponent<Clone>();

        cloneScript.snapshotArray = new List<PlayerSnapshot>(snapshotArray);
        cloneScript.eventArray = new List<PlayerEvent>(eventArray);
    }

    public void SetSnapshotArray(List<PlayerSnapshot> newSnapshotArray)
    {
        snapshotArray = new List<PlayerSnapshot>(newSnapshotArray);
    }
    public void SetEventArray(List<PlayerEvent> newEventArray)
    {
        eventArray = new List<PlayerEvent>(newEventArray);
    }
}

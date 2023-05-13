using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cloning : MonoBehaviour
{
    public Recorder recorder;
    
    public GameObject clonePrefab;

    public InputActionAsset actions;
    public InputAction recordButton, playButton;

    List<PlayerSnapshot> snapshotArray;

    private void Start()
    {
        recordButton = actions.FindActionMap("recorder", true).FindAction("record", true);
        playButton = actions.FindActionMap("recorder", true).FindAction("play", true);
        actions.FindActionMap("recorder").Enable();

        recorder = GameObject.FindGameObjectWithTag("Player").GetComponent<Recorder>();
    }

    void Update()
    {
        if ((playButton.WasPressedThisFrame() && !recorder.isRecording))
        {
            if (recorder.snapshotArray.Count > 0)
                SpawnClone();
            else
            {
                Debug.Log("No snapshots to play");
            }
        }
    }

    void SpawnClone()
    {
        // Get first position and rotation
        Vector3 initialPosition = snapshotArray[0].position;
        Quaternion initialRotation = snapshotArray[0].rotation;

        GameObject clone = Instantiate(clonePrefab, initialPosition, initialRotation);

        Clone cloneScript = clone.GetComponent<Clone>();

        cloneScript.snapshotArray = new List<PlayerSnapshot>(snapshotArray);
    }

    public void SetSnapshotArray(List<PlayerSnapshot> newSnapshotArray)
    {
        snapshotArray = new List<PlayerSnapshot>(newSnapshotArray);
    }
}

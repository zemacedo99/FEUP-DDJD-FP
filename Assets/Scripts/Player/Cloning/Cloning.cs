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

    private void Start()
    {
        recordButton = actions.FindActionMap("recorder", true).FindAction("record", true);
        playButton = actions.FindActionMap("recorder", true).FindAction("play", true);
        actions.FindActionMap("recorder").Enable();

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
        Vector3 initialPosition = recorder.snapshotArray[0].position;
        Quaternion initialRotation = recorder.snapshotArray[0].rotation;

        GameObject clone = Instantiate(clonePrefab, initialPosition, initialRotation);

        Clone cloneScript = clone.GetComponent<Clone>();

        cloneScript.snapshotArray = new List<PlayerSnapshot>(recorder.snapshotArray);
    }
}

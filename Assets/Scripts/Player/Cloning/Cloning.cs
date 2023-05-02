using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloning : MonoBehaviour
{
    public Recorder recorder;
    
    public GameObject clonePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !recorder.isRecording)
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
        // get first position
        Vector3 firstPosition = recorder.snapshotArray[0].position;

        GameObject clone = Instantiate(clonePrefab, firstPosition, Quaternion.identity);

        Clone cloneScript = clone.GetComponent<Clone>();

        cloneScript.snapshotArray = new List<PlayerSnapshot>(recorder.snapshotArray);
    }
}

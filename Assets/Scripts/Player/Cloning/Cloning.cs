using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloning : MonoBehaviour
{
    public Recorder recorder;
    
    public GameObject clonePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnClone();
        }
    }

    void SpawnClone()
    {
        // get first position
        Vector3 firstPosition = recorder.eventArray[0].position;

        GameObject clone = Instantiate(clonePrefab, firstPosition, Quaternion.identity);

        Clone cloneScript = clone.GetComponent<Clone>();

        cloneScript.actions = new List<PlayerAction>(recorder.eventArray);
        cloneScript.StartPlayback();
    }
}

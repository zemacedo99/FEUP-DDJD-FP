using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    Recorder recorder;

    // Start is called before the first frame update
    void Start()
    {
        recorder = gameObject.GetComponent<Recorder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            recorder.StartRecording();
        }else if (Input.GetMouseButtonUp(1))
        {
            recorder.StopRecording();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            recorder.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    Recorder recorder;
    Cloning cloningScript;

    // Start is called before the first frame update
    void Start()
    {
        recorder = gameObject.GetComponent<Recorder>();
        cloningScript = gameObject.GetComponent<Cloning>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !cloningScript.isClone)
        {
            recorder.StartRecording();
        }
        else if (Input.GetMouseButtonUp(1) && !cloningScript.isClone)
        {
            recorder.StopRecording();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !cloningScript.isClone)
        {
            recorder.Play();
        }
    }
}

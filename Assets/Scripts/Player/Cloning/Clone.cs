using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    const double DOUBLE_MINIMUM_VALUE = 0.01;

    public List<PlayerSnapshot> snapshotArray;
    public Camera cloneCamera;

    Recorder recorder;
    float playbackStartTime;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    bool isGrounded;

    public FMODUnity.EventReference footstepsEvent;
    public FMODUnity.EventReference jumpEvent;

    Rigidbody rb;

    void Start()
    {
        recorder = GameObject.FindGameObjectWithTag("Player").GetComponent<Recorder>();

        StartCoroutine(Playback());
    }

    IEnumerator Playback()
    {
        Debug.Log("Playback started");
        Debug.Log("Playback snapshots: " + snapshotArray.Count);

        // Enable camera
        //cloneCamera.enabled = true;

        int i = 0;
        float time = 0;
        playbackStartTime = Time.time;
        while (i < snapshotArray.Count - 1) {
            var currentSnapshot = snapshotArray[i];
            var nextSnapshot = snapshotArray[i + 1];

            // while waiting for next action interpolate everything in the time between the two actions
            while (time < currentSnapshot.timestamp) {
                transform.SetPositionAndRotation(Vector3.Lerp(currentSnapshot.position, nextSnapshot.position, time / nextSnapshot.timestamp), Quaternion.Lerp(currentSnapshot.rotation, nextSnapshot.rotation, time / nextSnapshot.timestamp));
                cloneCamera.transform.localRotation = Quaternion.Lerp(currentSnapshot.cameraRotation, nextSnapshot.cameraRotation, time / nextSnapshot.timestamp);

                isGrounded = Physics.CheckSphere(groundCheck.position, 0.35f, ground);

                yield return null;
                time += Time.deltaTime;
            }
            i++;
        }

        // Log playback time
        Debug.Log(Time.time - playbackStartTime);
        // Destroy clone
        Destroy(gameObject);
    }
}
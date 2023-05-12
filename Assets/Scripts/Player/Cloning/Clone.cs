using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    const double DOUBLE_MINIMUM_VALUE = 0.01;

    public List<PlayerSnapshot> snapshotArray;
    public List<PlayerEvent> eventArray;
    public Camera cloneCamera;

    Recorder recorder;
    float playbackStartTime;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    bool isGrounded;

    public FMODUnity.EventReference footstepsEvent;
    public FMODUnity.EventReference jumpEvent;

    float playTime;

    void Start()
    {
        recorder = GameObject.FindGameObjectWithTag("Player").GetComponent<Recorder>();

        StartCoroutine(Playback());
        StartCoroutine(ProcessEvents());
    }

    IEnumerator Playback()
    {
        Debug.Log("Playback started");
        Debug.Log("Playback snapshots: " + snapshotArray.Count);

        // Enable camera
        //cloneCamera.enabled = true;

        int snapshotIndex = 0;
        playTime = 0;
        playbackStartTime = Time.time;
        while (snapshotIndex < snapshotArray.Count - 1) {
            var currentSnapshot = snapshotArray[snapshotIndex];
            var nextSnapshot = snapshotArray[snapshotIndex + 1];

            // while waiting for next snapshot interpolate everything in the time between the two actions
            while (playTime < currentSnapshot.timestamp) {
                transform.SetPositionAndRotation(Vector3.Lerp(currentSnapshot.position, nextSnapshot.position, playTime / nextSnapshot.timestamp), Quaternion.Lerp(currentSnapshot.rotation, nextSnapshot.rotation, playTime / nextSnapshot.timestamp));
                cloneCamera.transform.localRotation = Quaternion.Lerp(currentSnapshot.cameraRotation, nextSnapshot.cameraRotation, playTime / nextSnapshot.timestamp);

                isGrounded = Physics.CheckSphere(groundCheck.position, 0.35f, ground);

                yield return null;
                playTime += Time.deltaTime;
            }
            transform.SetPositionAndRotation(currentSnapshot.position, currentSnapshot.rotation);
            cloneCamera.transform.localRotation = currentSnapshot.cameraRotation;
            snapshotIndex++;
        }

        // Log playback time
        Debug.Log(Time.time - playbackStartTime);
        // Destroy clone
        Destroy(gameObject);
    }

    IEnumerator ProcessEvents()
    {
        if (eventArray.Count == 0) yield break;

        int nextEventIndex = 0;
        while (nextEventIndex < eventArray.Count)
        {
            var nextEvent = eventArray[nextEventIndex];
            Debug.Log(playTime);
            Debug.Log(nextEvent.timestamp);
            if (playTime >= nextEvent.timestamp)
            {
                switch (nextEvent.type)
                {
                    case PlayerEvent.EventType.Jump:
                        Debug.Log("Jump Event");
                        FMODUnity.RuntimeManager.PlayOneShotAttached(jumpEvent, gameObject);
                        break;
                    case PlayerEvent.EventType.FootstepsSound:
                        FMODUnity.RuntimeManager.PlayOneShotAttached(footstepsEvent, gameObject);
                        break;
                }
                nextEventIndex++;
            }
            yield return null;
        }
    }
}
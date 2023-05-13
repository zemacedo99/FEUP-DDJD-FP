using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    public List<PlayerSnapshot> snapshotArray;
    public Camera cloneCamera;

    Recorder recorder;
    float playbackStartTime;

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
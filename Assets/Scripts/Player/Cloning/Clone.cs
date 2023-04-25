using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    public List<PlayerAction> actions;
    public Camera cloneCamera;

    public void StartPlayback()
    {
        StartCoroutine(Playback());
    }

    IEnumerator Playback()
    {
        Debug.Log("Playback started");
        Debug.Log("Playback actions: " + actions.Count);

        // Enable camera
        cloneCamera.enabled = true;

        int i = 0;
        while (i < actions.Count - 1) {
            var currentAction = actions[i];
            var nextAction = actions[i + 1];

            // while waiting for next action interpolate everything in the time between the two actions
            float time = 0;
            while (time < currentAction.delay) {
                time += Time.deltaTime;

                transform.position = Vector3.Lerp(currentAction.position, nextAction.position, time / nextAction.delay);
                transform.rotation = Quaternion.Lerp(currentAction.rotation, nextAction.rotation, time / nextAction.delay);

                cloneCamera.transform.localRotation = Quaternion.Lerp(currentAction.cameraRotation, nextAction.cameraRotation, time / nextAction.delay);

                yield return null;
            }

            i++;
        }

        // Destroy clone
        Destroy(gameObject);
    }
}
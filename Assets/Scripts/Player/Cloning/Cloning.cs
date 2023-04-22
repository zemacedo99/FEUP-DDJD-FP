using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloning : MonoBehaviour
{
    public bool isClone = false;
    public Recorder recorder;

    PlayerMovement pm;

    public void InitClone()
    {
        isClone = true;

        // Get PlayerMovement script
        pm = gameObject.GetComponent<PlayerMovement>();

        // Get recorder script of the respective cube
        // Atm gets from Player
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        recorder = playerGameObject.GetComponent<Recorder>();

        // Remove MainCamera (the Player camera)
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.CompareTag("MainCamera"))
                Destroy(child);
        }
        

        //// Add the clone camera
        ////      Make cloneCamera a child of this gameobject
        //GameObject cloneCamera = Instantiate(recorder.startingCamera, recorder.startingCamera.transform.localPosition, recorder.startingCamera.transform.localRotation);
        //cloneCamera.transform.SetParent(gameObject.transform, false);
        //Debug.Log(cloneCamera.transform.localRotation);
        ////      Set it in the PlayerMovement script
        //pm.playerCamera = cloneCamera.transform;
        //Debug.Log(pm.playerCamera.localRotation);
        //cloneCamera.GetComponent<Camera>().enabled = true;

    }
}

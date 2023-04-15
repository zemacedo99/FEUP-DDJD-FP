using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloning : MonoBehaviour
{
    public bool isClone = false;
    public GameObject startingCamera;

    public void InitClone(GameObject newStartingCamera)
    {
        isClone = true;

        // Replace clone Camera and deactivate it
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            if (gameObject.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Cameras"))
                Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        startingCamera = Instantiate(newStartingCamera, gameObject.transform);
        startingCamera.GetComponent<Camera>().enabled = false;
    }
}

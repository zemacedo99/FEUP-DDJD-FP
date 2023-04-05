using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloning : MonoBehaviour
{
    public bool isClone = false;

    public void InitClone(GameObject startingCamera)
    {
        isClone = true;

        PlayerActions playerActionsScript = GetComponent<PlayerActions>();
        Destroy(playerActionsScript);

        // Replace clone Camera and deactivate it
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            if (gameObject.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Cameras"))
                Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        GameObject camera = Instantiate(startingCamera, gameObject.transform);
        camera.transform.parent = gameObject.transform;
        camera.GetComponent<Camera>().enabled = false;
    }
}

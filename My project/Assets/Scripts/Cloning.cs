using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloning : MonoBehaviour
{
    public bool isClone = false;

    public void InitClone()
    {
        // Init Clone
        isClone = true;
        // compare children of game object
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            // only destroy tagged object
            if (gameObject.transform.GetChild(i).gameObject.CompareTag("MainCamera"))
                Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}

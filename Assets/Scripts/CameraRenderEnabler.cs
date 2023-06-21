using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRenderEnabler : MonoBehaviour
{
    public GameObject obj;
    void OnPreCull()
    {
        obj.SetActive(true);
    }

    void OnPreRender()
    {
        obj.SetActive(true);
    }
    void OnPostRender()
    {
        obj.SetActive(false);
    }
}

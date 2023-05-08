using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("CUBE SCRIPT RUNNING");

    }

    private void OnMouseOver()
    {
        // Glow...
        Debug.Log("Mouse Over");

        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject);
    }
}

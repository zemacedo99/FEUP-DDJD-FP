using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    float scaleFactor;

    private void Start()
    {
        scaleFactor = 1.3f;

    }

    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject);
    }
    private void OnMouseEnter()
    {
        // Glow...
        Debug.Log("Mouse Enter");

        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

    }
    private void OnMouseExit()
    {
        // Stop Glow...
        Debug.Log("Mouse Exit");

        transform.localScale = new Vector3(1, 1, 1);
    }
}

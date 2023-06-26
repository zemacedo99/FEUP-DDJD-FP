using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeScript : MonoBehaviour
{
    public InputActionAsset actions;
    public InputAction destroyCubeButton;

    float scaleFactor;

    private void Start()
    {
        actions.FindActionMap("interactions").Enable();
        destroyCubeButton = actions.FindActionMap("interactions", true).FindAction("destroyCube", true);

        scaleFactor = 1.3f;

    }

    private void OnMouseOver()
    {
        if (destroyCubeButton.WasPressedThisFrame())
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

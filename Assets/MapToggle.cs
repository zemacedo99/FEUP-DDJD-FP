using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapToggle : MonoBehaviour
{
    public GameObject mapCanvas;
    public InputActionAsset actions;
    private InputAction toggleMapAction;
    private bool isMapActive = false;

    private void OnEnable()
    {
        toggleMapAction.Enable();
    }

    private void OnDisable()
    {
        toggleMapAction.Disable();
    }

    private void Awake()
    {
        actions.FindActionMap("interactions").Enable();
        toggleMapAction = actions.FindActionMap("interactions", true).FindAction("map", true);
    }

    private void Update()
    {
        if (toggleMapAction.WasPressedThisFrame())
        {
            ToggleMap();
        }
    }

    private void ToggleMap()
    {
        isMapActive = !isMapActive;
        mapCanvas.SetActive(isMapActive);
    }

}

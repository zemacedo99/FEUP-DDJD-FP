using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapToggle : MonoBehaviour
{
    public GameObject mapCanvas;
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
        toggleMapAction = new InputAction("interactions/map", binding: "<Keyboard>/m");
        toggleMapAction.performed += ToggleMap;
    }

    private void ToggleMap(InputAction.CallbackContext context)
    {
        isMapActive = !isMapActive;
        mapCanvas.SetActive(isMapActive);
    }

}

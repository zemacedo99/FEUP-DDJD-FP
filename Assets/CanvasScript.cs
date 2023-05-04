using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public InputActionAsset actions;
    public InputAction pauseInput;
    public bool pauseMenuDisplay;

    void Start()
    {
        pauseMenuDisplay = false;
        actions.FindActionMap("interactions").Enable();
        pauseInput = actions.FindActionMap("interactions", true).FindAction("pause", true);
    }

    void Update()
    {
        if (pauseInput.WasPressedThisFrame())
        {
            pauseMenuDisplay = !pauseMenuDisplay;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().stopMove = pauseMenuDisplay;
            PauseMenu.SetActive(pauseMenuDisplay);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasScript : MonoBehaviour
{
    public InputActionAsset actions;
    public InputAction pauseInput;
    public InputAction inventoryInput;
    public bool pauseMenuIsDisplay, inventoryIsDisplay;

    void Start()
    {
        pauseMenuIsDisplay = false;
        actions.FindActionMap("interactions").Enable();
        pauseInput = actions.FindActionMap("interactions", true).FindAction("pause", true);
        inventoryInput = actions.FindActionMap("interactions", true).FindAction("inventory", true);
    }

    public GameObject GetChildByName(string _name)
    {
        return this.transform.Find(_name).gameObject;
    }

    public void StopPlayerMovement()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().stopMove = pauseMenuIsDisplay;
    }

    void Update()
    {
        if (pauseInput.WasPressedThisFrame())
        {
            // Force close the inventory
            inventoryIsDisplay = false;
            this.GetChildByName("InventoryScreen").SetActive(inventoryIsDisplay);

            pauseMenuIsDisplay = !pauseMenuIsDisplay;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().stopMove = pauseMenuIsDisplay;
            this.GetChildByName("PauseMenu").SetActive(pauseMenuIsDisplay);
        }
        if (inventoryInput.WasPressedThisFrame() && !pauseMenuIsDisplay)
        {
            inventoryIsDisplay = !inventoryIsDisplay;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().stopMove = inventoryIsDisplay;
            this.GetChildByName("InventoryScreen").SetActive(inventoryIsDisplay);
        }

    }
}

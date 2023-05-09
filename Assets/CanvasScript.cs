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

    public void ActivatePauseMenu(bool _isActivate)
    {   
        pauseMenuIsDisplay = _isActivate;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().stopMove = pauseMenuIsDisplay;
        this.GetChildByName("PauseMenu").SetActive(pauseMenuIsDisplay);
    }

    public void ActivateInventory(bool _isActivate)
    {
        inventoryIsDisplay = _isActivate;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().stopMove = inventoryIsDisplay;
        this.GetChildByName("InventoryScreen").SetActive(inventoryIsDisplay);
    }

    void Update()
    {
        if (pauseInput.WasPressedThisFrame())
        {
            if (this.transform.Find("PauseMenu").GetComponent<PuzzlePauseMenuScript>() != null && this.transform.Find("PauseMenu").GetComponent<PuzzlePauseMenuScript>().isWarningScreen)
            {
                print("yes");
                this.transform.Find("PauseMenu").GetComponent<PuzzlePauseMenuScript>().DisableWarningScreen();
                return;
            }
            // Force close the inventory
            inventoryIsDisplay = false;
            this.GetChildByName("InventoryScreen").SetActive(inventoryIsDisplay);

            ActivatePauseMenu(!pauseMenuIsDisplay);
        }
        if (inventoryInput.WasPressedThisFrame() && !pauseMenuIsDisplay)
        {
            ActivateInventory(!inventoryIsDisplay);
        }

    }
}

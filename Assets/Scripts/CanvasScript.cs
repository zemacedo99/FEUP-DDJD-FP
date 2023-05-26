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
    public bool isPaused = false;

    public FMODUnity.EventReference goBack;

    void Start()
    {
        pauseMenuIsDisplay = false;
        actions.FindActionMap("interactions").Enable();
        pauseInput = actions.FindActionMap("interactions", true).FindAction("pause", true);
        inventoryInput = actions.FindActionMap("interactions", true).FindAction("inventory", true);
    }

    public GameObject GetChildByName(string name)
    {
        return this.transform.Find(name).gameObject;
    }

    public void PauseMenuSetActive(bool isActive)
    {   
        pauseMenuIsDisplay = isActive;
        if (isActive)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        this.GetChildByName("PauseMenu").SetActive(isActive);
    }

    public void InventorySetActive(bool isActive)
    {
        inventoryIsDisplay = isActive;
        if (isActive)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        this.GetChildByName("InventoryScreen").SetActive(isActive);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
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

            PauseMenuSetActive(!pauseMenuIsDisplay);
        }
        if (inventoryInput.WasPressedThisFrame() && !pauseMenuIsDisplay)
        {
            InventorySetActive(!inventoryIsDisplay);
        }

    }
}

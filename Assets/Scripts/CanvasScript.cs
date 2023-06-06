using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasScript : MonoBehaviour
{
    public GameObject mapCamera;
    public InputActionAsset actions;
    public InputAction pauseInput;
    public InputAction inventoryInput;
    public InputAction mapInput;
    public bool pauseMenuIsDisplay, inventoryIsDisplay, mapIsDisplay;
    public bool isPaused = false;

    public FMODUnity.EventReference goBack;

    void Start()
    {
        actions.FindActionMap("interactions").Enable();
        pauseInput = actions.FindActionMap("interactions", true).FindAction("pause", true);
        inventoryInput = actions.FindActionMap("interactions", true).FindAction("inventory", true);
        mapInput = actions.FindActionMap("interactions", true).FindAction("map", true);
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

    public void MapSetActive(bool isActive)
    {
        mapIsDisplay = isActive;
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
        this.GetChildByName("MapWindow").SetActive(isActive);
        this.mapCamera.SetActive(isActive);
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
            InventorySetActive(false);
            MapSetActive(false);

            PauseMenuSetActive(!pauseMenuIsDisplay);
        }
        if (inventoryInput.WasPressedThisFrame() && !pauseMenuIsDisplay)
        {
            MapSetActive(false);
            InventorySetActive(!inventoryIsDisplay);
        }
        if (mapInput.WasPressedThisFrame() && !pauseMenuIsDisplay)
        {
            InventorySetActive(false);
            MapSetActive(!mapIsDisplay);
        }

    }
}

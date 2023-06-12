using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    public GameObject mapCamera;
    public InputActionAsset actions;
    public InputAction pauseInput;
    public InputAction inventoryInput;
    public InputAction mapInput;
    public bool pauseMenuIsDisplay, inventoryIsDisplay, mapIsDisplay, narrativeIsDisplay;
    public bool isPaused = false;
    public float eyeBlinkaAnimationTime = 10f;

    public FMODUnity.EventReference goBack;

    void Start()
    {
        actions.FindActionMap("interactions").Enable();
        pauseInput = actions.FindActionMap("interactions", true).FindAction("pause", true);
        inventoryInput = actions.FindActionMap("interactions", true).FindAction("inventory", true);
        mapInput = actions.FindActionMap("interactions", true).FindAction("map", true);

        PlayEyeBlink();
    }

    public void PlayEyeBlink()
    {
        if (SceneManager.GetActiveScene().name != "World")
        {
            eyeBlinkaAnimationTime = 0;
            return;
        }
        if (PlayerPrefs.GetInt("IsFirstTapeCollected") != 1 && !PlayerPrefs.HasKey("CheckpointX"))
        {
            // No Saved State
            isPaused = true;
            return;
        }
        eyeBlinkaAnimationTime = 0;
        GameObject.Find("EyeBlink").SetActive(false);
    }

    public GameObject GetChildByName(string name)
    {
        Transform objectTransform = this.transform.Find(name);
        if (!objectTransform)
        {
            return null;
        }

        return objectTransform.gameObject;
    }

    public void SetPause(bool isActive)
    {
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
    }

    public void PauseMenuSetActive(bool isActive)
    {   
        pauseMenuIsDisplay = isActive;

        SetPause(isActive);

        this.GetChildByName("PauseMenu").SetActive(isActive);

        if (isActive && SceneManager.GetActiveScene().name == "World")
        {
             
            this.GetChildByName("PauseMenu").GetComponent<PauseMenuScript>().UpdateCollectedTape();
        }
    }

    public void InventorySetActive(bool isActive)
    {
        inventoryIsDisplay = isActive;

        SetPause(isActive);

        this.GetChildByName("InventoryScreen").SetActive(isActive);
    }

    public void NarrativeSetSctive(bool isActive, ItemObject item = null)
    {
        narrativeIsDisplay = isActive;

        if (isActive)
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
        }

        this.GetChildByName("NarrativeScreen").SetActive(isActive);

        if (isActive)
        {
            this.GetChildByName("NarrativeScreen").GetComponent<NarrativeScreenScript>().Init(item);
        }
    }

    public void MapSetActive(bool isActive)
    {
        GameObject mapWindowObject = this.GetChildByName("MapWindow");

        if (!mapWindowObject)
        {
            return;
        }

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
        if (eyeBlinkaAnimationTime > 0)
        {
            eyeBlinkaAnimationTime = eyeBlinkaAnimationTime - Time.deltaTime;
            if(eyeBlinkaAnimationTime <= 0)
            {
                isPaused = false;
            }
            return;
        }

        if (pauseInput.WasPressedThisFrame())
        {
            if (this.transform.Find("PauseMenu").GetComponent<PuzzlePauseMenuScript>() != null && this.transform.Find("PauseMenu").GetComponent<PuzzlePauseMenuScript>().isWarningScreen)
            {
                this.transform.Find("PauseMenu").GetComponent<PuzzlePauseMenuScript>().DisableWarningScreen();
                return;
            }
            // Force close the inventory and narrative
            InventorySetActive(false);
            MapSetActive(false);
            narrativeIsDisplay = false;
            this.GetChildByName("NarrativeScreen").SetActive(narrativeIsDisplay);

            PauseMenuSetActive(!pauseMenuIsDisplay);
        }
        if (inventoryInput.WasPressedThisFrame() && !pauseMenuIsDisplay && !narrativeIsDisplay)
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

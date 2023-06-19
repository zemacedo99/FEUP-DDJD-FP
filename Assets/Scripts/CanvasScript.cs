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

    public FMODUnity.EventReference pauseSnapshot;
    FMOD.Studio.EventInstance pauseSnapshotInstance;

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
        if (PlayerPrefs.GetInt("IsFirstTapeCollected") != 1 && !PlayerPrefs.HasKey("CheckpointX") && PlayerPrefs.GetInt("Puzzle") != 1)
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

    public void SetPause(bool isActive, bool stopTime = true)
    {
        if (isActive)
        {
            if (stopTime)
                Time.timeScale = 0;
            isPaused = true;
            pauseSnapshotInstance = FMODUnity.RuntimeManager.CreateInstance(pauseSnapshot);
            pauseSnapshotInstance.start();
            print("Paused");
            var notifs = FindObjectsOfType<NotificationFlash>();
            foreach (NotificationFlash notif in notifs)
                notif.Kill();
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseSnapshotInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            print("UnPaused");

        }
    }

    public void PauseMenuSetActive(bool isActive)
    {   
        pauseMenuIsDisplay = isActive;

        this.GetChildByName("PauseMenu").SetActive(isActive);

        if (isActive && SceneManager.GetActiveScene().name == "World")
        {
             
            this.GetChildByName("PauseMenu").GetComponent<PauseMenuScript>().UpdateCollectedTape();
        }

        SetPause(isActive);
    }

    public void InventorySetActive(bool isActive)
    {
        inventoryIsDisplay = isActive;

        this.GetChildByName("InventoryScreen").SetActive(isActive);

        SetPause(isActive);
    }

    public void NarrativeSetSctive(bool isActive, ItemObject item = null)
    {
        narrativeIsDisplay = isActive;

        this.GetChildByName("NarrativeScreen").SetActive(isActive);

        if (isActive)
        {
            this.GetChildByName("NarrativeScreen").GetComponent<NarrativeScreenScript>().Init(item);
        }

        SetPause(isActive, false);
    }

    public void MapSetActive(bool isActive)
    {
        GameObject mapWindowObject = this.GetChildByName("MapWindow");

        if (!mapWindowObject)
        {
            return;
        }

        mapIsDisplay = isActive;

        this.GetChildByName("MapWindow").SetActive(isActive);
        this.mapCamera.SetActive(isActive);

        SetPause(isActive);
    }

    private void OnDestroy()
    {
        SetPause(false);
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

        if (pauseInput.WasPressedThisFrame() && !narrativeIsDisplay)
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
        if (mapInput.WasPressedThisFrame() && !pauseMenuIsDisplay && !narrativeIsDisplay)
        {
            InventorySetActive(false);
            MapSetActive(!mapIsDisplay);
        }

    }
}

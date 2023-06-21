using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public InventoryScript inventory;
    public InputActionAsset actions;
    public InputAction pickupInput;
    public GameObject InventoryScreen;
    public GameObject PickUpMessage;
    public ItemObject firstTapeObject;
    private Item currentTouched = null;
    
    public FMODUnity.EventReference itemPickup;
    public FMODUnity.EventReference tapeStart;

    public NotificationFlash notif;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if(item)
        {
            PickUpMessage.SetActive(true);

            PickUpMessage.GetComponent<PickupMessageScript>().UpdateMessage(item);

            currentTouched = item;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            PickUpMessage.SetActive(false);
            currentTouched = null;
        }
    }

    private void Start()
    {
        actions.FindActionMap("interactions").Enable();
        pickupInput = actions.FindActionMap("interactions", true).FindAction("pickup", true);
        if(PlayerPrefs.GetInt("IsFirstTapeCollected") == 1 && !HasItem("Strange Tape I"))
        {
            inventory.AddItem(firstTapeObject, 1);
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.HasItem(itemName);
    }

    public bool HasItem(ItemObject item)
    {
        return inventory.HasItem(item.itemName);
    }

    public void TriggerDoorOpen()
    {
        if (SceneManager.GetActiveScene().name == "World")
            return;

        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject door in doors)
        {
            door.GetComponent<DoorsScript>().OpenDoorIfHasAlItems();
        }
    }

    public void ShowTutorial(string itemName)
    {
        switch (itemName)
        {
            case "Cloning Device":
                GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<CanvasScript>().TutorialSetActive(true, "Cloning Device Tutorial");
                return;
            case "Initial Tutorial":
                GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<CanvasScript>().TutorialSetActive(true, "Initial Tutorial");
                return;
            default:
                return;

        }
    }

    private void Update()
    {
        if (currentTouched && pickupInput.WasPressedThisFrame())
        {
            PickUpMessage.SetActive(false);
            inventory.AddItem(currentTouched.item, 1);
            Destroy(currentTouched.gameObject);
            if (currentTouched.item.type.ToString() == "CassettePlayer")
            {
                if (currentTouched.item.itemName == "Strange Tape I")
                {
                    PlayerPrefs.SetInt("IsFirstTapeCollected", 1);
                    inventory.Save();
                }
                GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<CanvasScript>().NarrativeSetSctive(true, currentTouched.gameObject.GetComponent<Item>().item);

                FMODUnity.RuntimeManager.PlayOneShot(tapeStart);
            } else
            {
                ShowTutorial(currentTouched.item.itemName);
                notif.Enable();
            }
            InventoryScreen.GetComponent<InventoryScreenScript>().UpdateInformationScreen();
            TriggerDoorOpen(); // Opens all the doors that needs items in order for it to open

            if (currentTouched.item.type.ToString() != "CassettePlayer")
                FMODUnity.RuntimeManager.PlayOneShot(itemPickup);

            currentTouched = null;
        }

        return;
    }
}

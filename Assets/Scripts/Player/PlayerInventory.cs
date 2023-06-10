using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public InventoryScript inventory;
    public InputActionAsset actions;
    public InputAction pickupInput;
    public GameObject InventoryScreen;
    public GameObject PickUpMessage;
    private Item currentTouched = null;
    
    public FMODUnity.EventReference itemPickup; 

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
        //inventory.Load();
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
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject door in doors)
        {
            door.GetComponent<DoorsScript>().OpenDoorIfHasAlItems();
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
                GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<CanvasScript>().NarrativeSetSctive(true, currentTouched.gameObject.GetComponent<Item>().item);
            }
            currentTouched = null;

            InventoryScreen.GetComponent<InventoryScreenScript>().UpdateInformationScreen();
            TriggerDoorOpen(); // Opens all the doors that needs items in order for it to open

            FMODUnity.RuntimeManager.PlayOneShot(itemPickup);
        }

        return;
    }
}

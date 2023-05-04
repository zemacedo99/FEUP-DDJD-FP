using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public bool isDisplay = false;
    public InputActionAsset actions;
    public InputAction inventoryInput;
    public InputAction pickupInput;
    public GameObject InventoryScreen;
    public GameObject PickUpMessage;
    private Item currentTouched = null;

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
        inventoryInput = actions.FindActionMap("interactions", true).FindAction("inventory", true);
        pickupInput = actions.FindActionMap("interactions", true).FindAction("pickup", true);
    }

    private void Update()
    {
        if (inventoryInput.WasPressedThisFrame())
        {
            isDisplay = !isDisplay;
            InventoryScreen.SetActive(isDisplay);
        }
        if (currentTouched && pickupInput.WasPressedThisFrame())
        {
            PickUpMessage.SetActive(false);
            inventory.AddItem(currentTouched.item, 1);
            Destroy(currentTouched.gameObject);
            currentTouched = null;
        }
        
        return;
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}

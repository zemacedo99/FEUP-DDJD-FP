using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    //public InventoryObject inventory;
    public InventoryScript inventory;
    public InputActionAsset actions;
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
        print("started");
        actions.FindActionMap("interactions").Enable();
        pickupInput = actions.FindActionMap("interactions", true).FindAction("pickup", true);
        //inventory.Load();
    }

    private void Update()
    {
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
        inventory.Clear();
    }
}

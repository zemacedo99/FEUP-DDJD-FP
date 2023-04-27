using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public bool isDisplay = false;
    public InputActionAsset actions;
    public InputAction inventoryInput;
    public GameObject InventoryScreen;
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if(item)
        {
            print("captured!");
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void Start()
    {
        actions.FindActionMap("interactions").Enable();
        inventoryInput = actions.FindActionMap("interactions", true).FindAction("inventory", true);
    }

    private void Update()
    {
        if (inventoryInput.WasPressedThisFrame())
        {
            isDisplay = !isDisplay;
            InventoryScreen.SetActive(isDisplay);
        }

        return;
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}

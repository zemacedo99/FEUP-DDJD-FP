using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public InventoryObject inventory;

    void Start()
    {
        inventory.Load();
    }

    public void Load()
    {
        inventory.Load();
    }

    public void AddItem(ItemObject item, int amount)
    {
        inventory.AddItem(item, amount);
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    public bool HasItem(string itemName)
    {
        return inventory.HasItem(itemName);
    }

    public void Save()
    {
        inventory.Save();
    }
}

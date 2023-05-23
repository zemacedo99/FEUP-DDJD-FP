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

    public void Clear()
    {
        inventory.Container.Clear();
    }

    public void Save()
    {
        inventory.Save();
    }
}

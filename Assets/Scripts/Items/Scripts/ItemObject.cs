using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Equipment,
    Consumeble,
    CassettePlayer,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public string itemName;
    public ItemType type;
    [TextArea(2, 20)]
    public string instruction;
    [TextArea(15,20)]
    public string lore;
}

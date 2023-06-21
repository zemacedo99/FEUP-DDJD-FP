using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    private Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public int GetIdFunc(ItemObject item)
    {
        foreach (var key in GetId.Keys)
            if (key.itemName == item.itemName)
                return GetId[key];
        return 0;
    }


    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<ItemObject, int>();
        for (int i = 0; i < Items.Length; i++)
        {
            GetId.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventor")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    static private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].addAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlot(database.GetIdFunc(_item),_item, _amount));
    }

    public bool HasItem(string itemName)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].id];
        }
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
        database = Resources.Load<ItemDatabaseObject>("Database");
        //if(database == null)
        //    Addressables.LoadAssetAsync<ItemDatabaseObject>("Database").Completed += OnLoadDone;
    }

    private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<ItemDatabaseObject> obj)
    {
        Debug.Log(obj.Result);
        database = obj.Result;
    }


    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
        else
        {
            Container = new List<InventorySlot>();
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public int id;
    public InventorySlot(int _id,ItemObject _item, int _amount)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }
    public void setAmount(int _amount)
    {
        amount = _amount;
    }
    public void addAmount(int _amount)
    {
        amount += _amount;
    }
}
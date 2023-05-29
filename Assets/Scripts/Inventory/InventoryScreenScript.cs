using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;

public class InventoryScreenScript : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject emptySlotPrefab;
    public float X_START;
    public float Y_START;

    public float X_GAP;
    public float Y_GAP;

    public float NUMBER_OF_COLUMNS;
    public float NUMBER_OF_ROWS;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InitiateInventory();
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i])){
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var item = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                item.GetComponent<RectTransform>().localPosition = GetPosition(i);
                item.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], item);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var item = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            item.GetComponent<RectTransform>().localPosition = GetPosition(i);
            item.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.Container[i], item);

        }
    }
    public void InitiateInventory()
    {
        for (int i = 0; i < NUMBER_OF_COLUMNS* NUMBER_OF_ROWS; i++)
        {
            var emptySlot = Instantiate(emptySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            emptySlot.GetComponent<RectTransform>().localPosition = GetPosition(i);
            emptySlot.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public Vector3 GetPosition(int position)
    {
        return new Vector3(X_START + (X_GAP* (int)(position % NUMBER_OF_COLUMNS)), Y_START + (-1*Y_GAP * (int)(position/NUMBER_OF_COLUMNS)), 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.Editor.InputActionCodeGenerator;

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

    public int currentSeleted = 0;

    public InputActionAsset actions;

    public InputAction upInput, downInput, rightInput, leftInput;


    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InitCommands();
        InitiateInventory();
        CreateDisplay();
        UpdateSelectedItem(0);
        UpdateInformationScreen();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlotsDisplay();
        UpdateScreenMovement();
    }
    private void UpdateScreenMovement()
    {
        if (downInput.WasPressedThisFrame() && (currentSeleted / (int)NUMBER_OF_COLUMNS) < NUMBER_OF_ROWS - 1)
        {
            UpdateSelectedItem(currentSeleted + (int)NUMBER_OF_COLUMNS);
            UpdateInformationScreen();

            return;
        }
        if (upInput.WasPressedThisFrame() && currentSeleted >= NUMBER_OF_COLUMNS)
        {
            UpdateSelectedItem(currentSeleted - (int)NUMBER_OF_COLUMNS);
            UpdateInformationScreen();

            return;
        }
        if (rightInput.WasPressedThisFrame() && currentSeleted < (NUMBER_OF_COLUMNS * NUMBER_OF_ROWS - 1))
        {
            UpdateSelectedItem(currentSeleted + 1);
            UpdateInformationScreen();

            return;
        }
        if (leftInput.WasPressedThisFrame() && currentSeleted > 0)
        {
            UpdateSelectedItem(currentSeleted - 1);
            UpdateInformationScreen();

            return;
        }
    }

    public void UpdateInformationScreen()
    {
        if (inventory.Container.Count == 0 || currentSeleted >= inventory.Container.Count)
        {
            SetInventoryInfoScreen(itemName: "", itemLore: "", itemInstruction: "", itemImage: null);

            return;
        }

        ItemObject currentSelectedItem = inventory.Container[currentSeleted].item;
        SetInventoryInfoScreen(itemName: currentSelectedItem.name, itemLore: currentSelectedItem.lore, itemInstruction: currentSelectedItem.instruction, itemImage: currentSelectedItem.prefab.GetComponent<RawImage>().texture);
    }

    public void SetInventoryInfoScreen(string itemName, string itemLore, string itemInstruction, Texture itemImage)
    {
        Transform inventoryInfoScreen = gameObject.transform.Find("InventoryInfoScreen");

        inventoryInfoScreen.Find("ItemName").GetComponent<TextMeshProUGUI>().text = itemName;
        inventoryInfoScreen.Find("ItemLore").GetComponent<TextMeshProUGUI>().text = itemLore;
        inventoryInfoScreen.Find("ItemInstruction").GetComponent<TextMeshProUGUI>().text = itemInstruction;
        if (!itemImage)
        {
            inventoryInfoScreen.Find("ItemImage").gameObject.SetActive(false);
        }
        else
        {
            inventoryInfoScreen.Find("ItemImage").gameObject.SetActive(true);
            inventoryInfoScreen.Find("ItemImage").GetComponent<RawImage>().texture = itemImage;
        }
    }

    private void InitCommands()
    {
        actions.FindActionMap("menu interactions").Enable();
        downInput = actions.FindActionMap("menu interactions", true).FindAction("moveDown", true);
        upInput = actions.FindActionMap("menu interactions", true).FindAction("moveUp", true);
        rightInput = actions.FindActionMap("menu interactions", true).FindAction("moveRight", true);
        leftInput = actions.FindActionMap("menu interactions", true).FindAction("moveLeft", true);
    }

    public void UpdateSlotsDisplay()
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

    public void UpdateSelectedItem(int newPostionIndex)
    {
        gameObject.transform.GetChild(currentSeleted+2).transform.Find("SelectedCanvas").gameObject.SetActive(false);
        gameObject.transform.GetChild(newPostionIndex + 2).transform.Find("SelectedCanvas").gameObject.SetActive(true);

        currentSeleted = newPostionIndex;
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

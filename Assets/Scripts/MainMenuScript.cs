using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class MainMenuScript : MonoBehaviour
{
    enum options { CONTINUE, NEWGAME, OPTIONS, CREDITS, EXIT }
    enum warningType { RESET, ADANDON }
    private warningType type = warningType.RESET;
    private options selectedOption;
    public InputActionAsset actions;
    public GameObject player;

    public InputAction upInput, downInput, selectInput;

    // Start is called before the first frame update
    void Start()
    {
        actions.FindActionMap("menu interactions").Enable();
        downInput = actions.FindActionMap("menu interactions", true).FindAction("moveDown", true);
        upInput = actions.FindActionMap("menu interactions", true).FindAction("moveUp", true);
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("select", true);

        selectedOption = options.CONTINUE;
    }



    void PaintSelectedOption()
    {
        for (int i = 0; i < (int)options.EXIT + 1; i++)
        {
            this.transform.Find("Buttons").GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 100f / 255f);
            this.transform.Find("Buttons").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(253f / 255f, 233f / 255f, 204f / 255f, 1);
            if (i == (int)selectedOption)
            {
                this.transform.Find("Buttons").GetChild(i).GetComponent<Image>().color = new Color(253f / 255f, 233f / 255f, 204f / 255f, 1);
                this.transform.Find("Buttons").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            }
        }

    }

    //public void SetWarningText(int _type)
    //{
    //    if (_type == (int)warningType.RESET)
    //    {
    //        this.transform.GetChild(0).Find("WarningText").GetComponent<TextMeshProUGUI>().text = "Are you sure you want to reset the quest?";
    //        this.type = warningType.RESET;

    //    }
    //    if (_type == (int)warningType.ADANDON)
    //    {
    //        this.transform.GetChild(0).Find("WarningText").GetComponent<TextMeshProUGUI>().text = "Are you sure you want to abandon the quest?";
    //        this.type = warningType.ADANDON;
    //    }
    //}

    void DeleteInventoryFile()
    {
        string filename = ((InventoryObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Inventory/PlayerInventory.asset", typeof(InventoryObject))).savePath;
        if (File.Exists(string.Concat(Application.persistentDataPath, filename)))
        {
            File.Delete(string.Concat(Application.persistentDataPath, filename));
        }
    }

    void ExecuteSelectedOption()
    {
        switch (selectedOption)
        {
            case options.CONTINUE:
                SceneManager.LoadScene("World");
                break;
            case options.NEWGAME:
                //PlayerSaveSystem.ResetData();
                DeleteInventoryFile();
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("World");
                break;
            case options.OPTIONS:
                SceneManager.LoadScene("World");
                break;
            case options.CREDITS:
                SceneManager.LoadScene("World");
                break;
            case options.EXIT:
                SceneManager.LoadScene("World");
                break;
        }
    }

    void Update()
    {
        if (downInput.WasPressedThisFrame() && selectedOption < options.EXIT)
        {
            print("down");
            selectedOption += 1;
            PaintSelectedOption();
            return;
        }
        if (upInput.WasPressedThisFrame() && selectedOption > 0)
        {
            print("up");

            selectedOption -= 1;
            PaintSelectedOption();
            return;
        }
        if (selectInput.WasPressedThisFrame())
        {
            ExecuteSelectedOption();
            return;
        }
    }
}

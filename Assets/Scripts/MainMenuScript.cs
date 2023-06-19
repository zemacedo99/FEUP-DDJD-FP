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
    enum options { CONTINUE, NEWGAME, CREDITS, EXIT }
    enum warningType { RESET, ADANDON }
    private warningType type = warningType.RESET;
    private options selectedOption;
    public InputActionAsset actions;
    public GameObject player;

    public InputAction upInput, downInput, selectInput;
    private bool isWarningScreen = false;
    private bool hasSavedData = true;

    public FMODUnity.EventReference moveUpDown;
    public FMODUnity.EventReference select;

    // Start is called before the first frame update
    void Start()
    {
        actions.FindActionMap("menu interactions").Enable();
        downInput = actions.FindActionMap("menu interactions", true).FindAction("moveDown", true);
        upInput = actions.FindActionMap("menu interactions", true).FindAction("moveUp", true);
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("select", true);

        selectedOption = options.CONTINUE;
        CheckForSavedState();
    }

    void CheckForSavedState()
    {
        if (PlayerPrefs.GetInt("IsFirstTapeCollected") != 1 && !PlayerPrefs.HasKey("CheckpointX"))
        {
            selectedOption = options.NEWGAME;
            hasSavedData = false;
            PaintSelectedOption(false);
        }
    }

    void PaintSelectedOption(bool playSound = true)
    {
        for (int i = 0; i < (int)options.EXIT + 1; i++)
        {
            if(i == (int)options.CONTINUE && !hasSavedData)
            {
                this.transform.Find("Buttons").GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 100f / 255f);
                this.transform.Find("Buttons").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(253f / 255f, 233f / 255f, 204f / 255f, 20f/255f);
                continue;
            }
            this.transform.Find("Buttons").GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 100f / 255f);
            this.transform.Find("Buttons").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(253f / 255f, 233f / 255f, 204f / 255f, 1);
            if (i == (int)selectedOption)
            {
                this.transform.Find("Buttons").GetChild(i).GetComponent<Image>().color = new Color(253f / 255f, 233f / 255f, 204f / 255f, 1);
                this.transform.Find("Buttons").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            }
        }

        if (playSound)
            FMODUnity.RuntimeManager.PlayOneShot(moveUpDown);
    }

    void DeleteInventoryFile()
    {
        string filename = ((InventoryObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Inventory/PlayerInventory.asset", typeof(InventoryObject))).savePath;
        if (File.Exists(string.Concat(Application.persistentDataPath, filename)))
        {
            File.Delete(string.Concat(Application.persistentDataPath, filename));
        }
    }

    public void EnableWarningScreen(int _warningType)
    {
        isWarningScreen = true;
        GameObject warningScreen = this.transform.Find("WarningScreen").gameObject;
        warningScreen.SetActive(true);
        warningScreen.GetComponent<WarningScreenScript>().SetWarningText(_warningType);
    }

    public void DisableWarningScreen()
    {
        isWarningScreen = false;
        GameObject warningScreen = this.transform.Find("WarningScreen").gameObject;
        warningScreen.SetActive(false);
    }

    public void RestartNewGame()
    {
        DeleteInventoryFile();
        SaveSystem.DeleteFogOfWarData();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Intro");
    }

    void ExecuteSelectedOption()
    {
        FMODUnity.RuntimeManager.PlayOneShot(select);

        switch (selectedOption)
        {
            case options.CONTINUE:
                SceneManager.LoadScene("World");
                break;
            case options.NEWGAME:
                if (!hasSavedData)
                {
                    RestartNewGame();
                    return;
                }
                EnableWarningScreen(2);
                break;
            case options.CREDITS:
                SceneManager.LoadScene("Credits");
                break;
            case options.EXIT:
                print("QUIT GAME SELECTED");
                Application.Quit();
                break;
        }
    }

    void Update()
    {
        if (isWarningScreen)
        {
            return;
        }
        if (downInput.WasPressedThisFrame() && selectedOption < options.EXIT)
        {
            selectedOption += 1;
            PaintSelectedOption();
            return;
        }
        if (upInput.WasPressedThisFrame() && selectedOption > 0)
        {
            if (!hasSavedData && selectedOption == options.NEWGAME)
            {
                return;
            }
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

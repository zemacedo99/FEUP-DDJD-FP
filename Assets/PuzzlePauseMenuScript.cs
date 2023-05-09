using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PuzzlePauseMenuScript : MonoBehaviour
{
    enum options { CONTINUE, RESET_QUEST, ABANDON_QUEST, SETTINGS, LEAVE }
    private options selectedOption;
    public string missionDescription;
    public InputAction upInput, downInput, leftInput, rightInput, selectInput;
    public bool isWarningScreen;
    public InputActionAsset actions;

    void Start()
    {
        isWarningScreen = false;
        selectedOption = options.CONTINUE;
        actions.FindActionMap("menu interactions").Enable();
        downInput = actions.FindActionMap("menu interactions", true).FindAction("moveDown", true);
        upInput = actions.FindActionMap("menu interactions", true).FindAction("moveUp", true);
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("select", true);

        missionDescription = "Complete the puzzle";
    }

    void PaintSelectedOption()
    {
        for (int i = 0; i < (int)options.LEAVE + 1; i++)
        {
            this.transform.Find("Buttons").GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 100f / 255f);
            this.transform.Find("Buttons").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            if (i == (int)selectedOption)
            {
                this.transform.Find("Buttons").GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                this.transform.Find("Buttons").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);

            }
        }
    }

    public void EnableWarningScreen(string message)
    {
        isWarningScreen = true;
        GameObject warningScreen = this.transform.Find("WarningScreen").gameObject;
        warningScreen.SetActive(true);
        warningScreen.GetComponent<WarningScreenScript>().SetWarningText(message);
    }

    public void DisableWarningScreen() {
        isWarningScreen = false;
        GameObject warningScreen = this.transform.Find("WarningScreen").gameObject;
        warningScreen.SetActive(false);
    }


    void ExecuteSelectedOption()
    {
        switch (selectedOption)
        {
            case options.CONTINUE:
                this.GetComponentInParent<CanvasScript>().ActivatePauseMenu(false);
                break;
            case options.RESET_QUEST:
                //this.GetComponentInParent<CanvasScript>().ActivatePauseMenu(false);
                EnableWarningScreen("Are you sure you want to reset the quest?");

                print("RESET WAS PRESSED");
                break;
            case options.ABANDON_QUEST:
                EnableWarningScreen("Are you sure you want to abandon the quest?");

                //this.GetComponentInParent<CanvasScript>().ActivatePauseMenu(false);
                print("ABANDON_QUEST WAS PRESSED");
                break;

            case options.SETTINGS:
                this.GetComponentInParent<CanvasScript>().ActivatePauseMenu(false);
                print("SETTINGS WAS PRESSED");
                break;
            case options.LEAVE:
                Application.Quit();
                break;
        }
    }

    private void Update()
    {
        if (isWarningScreen)
        {
            return;
        }
        if (downInput.WasPressedThisFrame() && selectedOption < options.LEAVE)
        {
            selectedOption += 1;
            PaintSelectedOption();
            return;
        }
        if (upInput.WasPressedThisFrame() && selectedOption > 0)
        {
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

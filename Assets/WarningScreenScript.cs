using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using TMPro;
using UnityEngine.UI;


public class WarningScreenScript : MonoBehaviour
{
    enum options { NO, YES }
    enum warningType { RESET, ADANDON }
    private warningType type = warningType.RESET;
    private options selectedOption;
    public InputActionAsset actions;
    
    public InputAction leftInput, rightInput, selectInput;

    // Start is called before the first frame update
    void Start()
    {
        actions.FindActionMap("menu interactions").Enable();
        leftInput = actions.FindActionMap("menu interactions", true).FindAction("moveLeft", true);
        rightInput = actions.FindActionMap("menu interactions", true).FindAction("moveRight", true);
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("select", true);

        selectedOption = options.NO;
    }

    

    void PaintSelectedOption()
    {
        for (int i = 0; i < (int)options.YES + 1; i++)
        {
            this.transform.Find("Panel").GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 100f / 255f);
            this.transform.Find("Panel").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            if (i == (int)selectedOption)
            {
                this.transform.Find("Panel").GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                this.transform.Find("Panel").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            }
        }

    }

    public void SetWarningText(int _type)
    {
        if(_type == (int)warningType.RESET)
        {
            this.transform.GetChild(0).Find("WarningText").GetComponent<TextMeshProUGUI>().text = "Are you sure you want to reset the quest?";
            this.type = warningType.RESET;

        }
        if (_type == (int)warningType.ADANDON)
        {
            this.transform.GetChild(0).Find("WarningText").GetComponent<TextMeshProUGUI>().text = "Are you sure you want to abandon the quest?";
            this.type = warningType.ADANDON;
        }
    }

    void ExecuteSelectedOption()
    {
        switch (selectedOption)
        {
            case options.NO:
                this.GetComponentInParent<PuzzlePauseMenuScript>().DisableWarningScreen();
                break;
            case options.YES:
                this.GetComponentInParent<PuzzlePauseMenuScript>().DisableWarningScreen();
                if (this.type == warningType.RESET)
                {
                    this.GetComponentInParent<PuzzlePauseMenuScript>().ResetQuest();
                    break;
                }
                else
                {
                    this.GetComponentInParent<PuzzlePauseMenuScript>().AbandonQuest();
                    break;

                }

        }
    }

    void Update()
    {
        if (leftInput.WasPressedThisFrame() && selectedOption != options.NO)
        {
            selectedOption = options.NO;
            PaintSelectedOption();
            return;
        }
        if (rightInput.WasPressedThisFrame() && selectedOption != options.YES)
        {
            selectedOption = options.YES;
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

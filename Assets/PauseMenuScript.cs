using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    enum options { CONTINUE, SETTINGS, LEAVE }
    private options selectedOption;
    public string missionDescription;
    public InputAction upInput, downInput, selectInput;
    public InputActionAsset actions;

    void Start()
    {

        selectedOption = options.CONTINUE;
        actions.FindActionMap("menu interactions").Enable();
        downInput = actions.FindActionMap("menu interactions", true).FindAction("moveDown", true);
        upInput = actions.FindActionMap("menu interactions", true).FindAction("moveUp", true);
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("select", true);

        missionDescription = "Escape the planet\nCollect Spaceship Pieces(0 / 6)";
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

    void ExecuteSelectedOption()
    {
        switch (selectedOption)
        {
            case options.CONTINUE:
                this.GetComponentInParent<CanvasScript>().ActivatePauseMenu(false);
                break;
            case options.SETTINGS:
                this.GetComponentInParent<CanvasScript>().ActivatePauseMenu(false);
                print("SETTINGS WAS PRESSED");
                break;
            case options.LEAVE:
                PlayerSaveSystem.Save();
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    private void Update()
    {
   
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

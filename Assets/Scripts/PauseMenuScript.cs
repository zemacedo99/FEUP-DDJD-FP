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
    enum options { CONTINUE, SETTINGS, GO_TO_MAINMENU }
    private options selectedOption;
    public string missionDescription;
    public InputAction upInput, downInput, selectInput;
    public InputActionAsset actions;
    public TextMeshProUGUI missionDescriptionText;
    private int collectedTapes;

    public FMODUnity.EventReference moveUpDown;
    public FMODUnity.EventReference select;

    void Start()
    {

        selectedOption = options.CONTINUE;
        actions.FindActionMap("menu interactions").Enable();
        downInput = actions.FindActionMap("menu interactions", true).FindAction("moveDown", true);
        upInput = actions.FindActionMap("menu interactions", true).FindAction("moveUp", true);
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("select", true);
        //collectedTapes = 0;

        UpdateCollectedTape();
    }

    void PaintSelectedOption()
    {
        for (int i = 0; i < (int)options.GO_TO_MAINMENU + 1; i++)
        {
            this.transform.Find("Buttons").GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 100f / 255f);
            this.transform.Find("Buttons").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            if (i == (int)selectedOption)
            {
                this.transform.Find("Buttons").GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                this.transform.Find("Buttons").GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);

            }
        }

        FMODUnity.RuntimeManager.PlayOneShot(moveUpDown);
    }

    public void UpdateCollectedTape()
    {
        if (collectedTapes == 6)
        {
            missionDescriptionText.text = "Return to the spacheschip to escape from Luminar";
            return;
        }
        string[] romanLetter = { "I", "II", "III", "IV", "V", "VI" };
        int count = 0;
        for (int i = 0; i < 6; i++)
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().inventory.HasItem("Strange Tape " + romanLetter[i]))
            {
                count+=1;
            }
        }
        collectedTapes = count;
        missionDescriptionText.text = " Collect all the Tapes (" + collectedTapes.ToString() + " / 6)";
    }

    void ExecuteSelectedOption()
    {
        FMODUnity.RuntimeManager.PlayOneShot(select);

        switch (selectedOption)
        {
            case options.CONTINUE:
                this.GetComponentInParent<CanvasScript>().PauseMenuSetActive(false);
                break;
            case options.SETTINGS:
                this.GetComponentInParent<CanvasScript>().PauseMenuSetActive(false);
                break;
            case options.GO_TO_MAINMENU:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    private void Update()
    {
   
        if (downInput.WasPressedThisFrame() && selectedOption < options.GO_TO_MAINMENU)
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

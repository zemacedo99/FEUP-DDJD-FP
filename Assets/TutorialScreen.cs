using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class TutorialScreen : MonoBehaviour
{
    public List<TutorialPages> tutorials = new List<TutorialPages>();
    public int currentTutorialIndex;
    public int currentPage;

    public InputAction rightInput, leftInput, skipInput;
    public InputActionAsset actions;

    // Start is called before the first frame update
    void Start()
    {
        currentTutorialIndex = 0;
        currentPage = 0;

        actions.FindActionMap("menu interactions").Enable();
        rightInput = actions.FindActionMap("menu interactions", true).FindAction("moveRight", true);
        leftInput = actions.FindActionMap("menu interactions", true).FindAction("moveLeft", true);
        skipInput = actions.FindActionMap("menu interactions", true).FindAction("skip", true);
    }

    public void UpdateTutorial(string tutorialName)
    {
        for (int i = 0; i < tutorials.Capacity; i++)
        {
            if (tutorials[i].tutorialPageName == tutorialName)
            {
                currentTutorialIndex = i;
                break;
            }
        }
        currentPage = 0;
        UpdateScreen();
        return;
    }

    void UpdateScreen()
    {
        GameObject.Find("Title").GetComponent<TextMeshProUGUI>().text = tutorials[currentTutorialIndex].tutorials[currentPage].title;
        GameObject.Find("Description").GetComponent<TextMeshProUGUI>().text = tutorials[currentTutorialIndex].tutorials[currentPage].description;

        GameObject.Find("ImageContainer").transform.Find("Image").GetComponent<Image>().sprite = tutorials[currentTutorialIndex].tutorials[currentPage].image;

        GameObject.Find("Label").transform.GetComponent<TextMeshProUGUI>().text = tutorials[currentTutorialIndex].tutorialPageName;

        print(tutorials[currentTutorialIndex].tutorialPageName);

        UpdateArrows();

    }

    void UpdateArrows()
    {
        Color disableColor = new Color(65f / 255f, 65f / 255f, 65f / 255f, 1f);
        Color activeColor = new Color(125f / 255f, 231f / 255f, 1f, 1f);



        if (currentPage == 0)
        {
            GameObject.Find("LeftArrow").GetComponent<Image>().color = disableColor;
        }
        else
        {
            GameObject.Find("LeftArrow").GetComponent<Image>().color = activeColor;
        }

        if (currentPage < tutorials[currentTutorialIndex].tutorials.Count - 1)
        {
            GameObject.Find("RightArrow").GetComponent<Image>().color = activeColor;

        }
        else
        {
            GameObject.Find("RightArrow").GetComponent<Image>().color = disableColor;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if ((rightInput.WasPressedThisFrame() || skipInput.WasPressedThisFrame()) && (currentPage < tutorials[currentTutorialIndex].tutorials.Count-1) )
        {
            currentPage++;
            UpdateScreen();
            return;
        }
        if (leftInput.WasPressedThisFrame() && (currentPage > 0))
        {
            currentPage--;
            UpdateScreen();
            return;
        }
    }
}

[Serializable]
public class TutorialPages
{ 
    public string tutorialPageName;
    public List<TutorialObject> tutorials = new List<TutorialObject>();

}

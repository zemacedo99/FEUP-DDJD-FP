using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class NarrativeScreenScript : MonoBehaviour
{
    [TextArea(2, 20)]
    private float textSpeed;
    public int lettersPerSecond;
    public TextMeshProUGUI textComponent;
    public int initialScreenAlpha;
    public int finalScreenAlpha;
    public float screenFlashSpeed;
    public TextMeshProUGUI instructionTextComponent;
    private string toBeDisplayedContent;

    private Coroutine screenFlashRoutine;
    private Coroutine typeLineRoutine;


    public InputAction selectInput;
    public InputActionAsset actions;


    // Start is called before the first frame update
    void Start()
    {
        actions.FindActionMap("menu interactions").Enable();
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("select", true);
        textSpeed = 1.0f / lettersPerSecond;
        Reset();
    }

    private void Reset()
    {
        textComponent.text = string.Empty;
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        instructionTextComponent.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (selectInput.WasPressedThisFrame())
        {
            if (toBeDisplayedContent == textComponent.text)
            {
                Reset();
                GetComponentInParent<CanvasScript>().NarrativeSetSctive(false, null);
            }
            else
            {
                StopCoroutine(screenFlashRoutine);
                StopCoroutine(typeLineRoutine);
                textComponent.text = toBeDisplayedContent;
                instructionTextComponent.gameObject.SetActive(true);
                gameObject.GetComponent<Image>().color = new Color(0, 0, 0, finalScreenAlpha / 255.0f);
            }
        }
    }

    public void Init(ItemObject item)
    {
        

        toBeDisplayedContent = item.lore;
        screenFlashRoutine = StartCoroutine(ScreenFlash());
        typeLineRoutine = StartCoroutine(TypeLine());
    }

    IEnumerator WaitTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator ScreenFlash() {
        for(int i = initialScreenAlpha; i <= finalScreenAlpha; i++)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, i/255.0f);
            yield return new WaitForSeconds(screenFlashSpeed);
        }
    }


    IEnumerator TypeLine()
    {

        char[] line = toBeDisplayedContent.ToCharArray();
        for (int i = -1; i <= line.Length; i++)
        {
            if (i == -1)
            {
                yield return new WaitForSeconds((finalScreenAlpha - initialScreenAlpha) * screenFlashSpeed + 2.0f);
            }
            else if (i == line.Length)
            {
                instructionTextComponent.gameObject.SetActive(true);
                yield return new WaitForSeconds(textSpeed);
            }
            else
            {
                textComponent.text += line[i];
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

}

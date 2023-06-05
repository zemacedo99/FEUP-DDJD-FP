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
    public string[] chapters;
    private float textSpeed;
    public int lettersPerSecond;
    public TextMeshProUGUI textComponent;
    public int initialScreenAlpha;
    public int finalScreenAlpha;
    public float screenFlashSpeed; 
    private int chapter;

    public InputAction selectInput;
    public InputActionAsset actions;


    // Start is called before the first frame update
    void Start()
    {
        actions.FindActionMap("menu interactions").Enable();
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("select", true);
        textComponent.text = string.Empty;
        textSpeed = 1.0f / lettersPerSecond;
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectInput.WasPressedThisFrame())
        {
            print("space pressed");
            if (chapters[chapter] == textComponent.text)
            {
                gameObject.SetActive(false);
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = chapters[chapter];
                gameObject.GetComponent<Image>().color = new Color(0, 0, 0, finalScreenAlpha / 255.0f);
            }
        }
    }

    public void Init(int _chapter = 0)
    {
        chapter = _chapter;
        StartCoroutine(ScreenFlash());
        StartCoroutine(TypeLine());
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
        char[] line = chapters[chapter].ToCharArray();
        for (int i = -1; i < line.Length; i++)
        {
            if(i == -1)
            {
                yield return new WaitForSeconds((finalScreenAlpha - initialScreenAlpha) * screenFlashSpeed + 2.0f);
            }
            else
            {
                textComponent.text += line[i];
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    //void NextChapter()
    //{
    //    if (chapter < lines.Length - 1)
    //    {
    //        chapter++;
    //        textComponent.text = string.Empty;
    //        StartCoroutine(TypeLine());
    //    }
    //}
}

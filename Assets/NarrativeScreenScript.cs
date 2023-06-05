using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class NarrativeScreenScript : MonoBehaviour
{
    public string[] lines;
    private float textSpeed;
    public int lettersPerSecond;
    public TextMeshProUGUI textComponent;
    public int initialScreenAlpha;
    public int finalScreenAlpha;
    public float screenFlashSpeed; 
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        textSpeed = 1.0f / lettersPerSecond;
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
        index = 0;
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
        char[] line = lines[index].ToCharArray();
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

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
}

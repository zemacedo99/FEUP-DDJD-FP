using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBarScript : MonoBehaviour
{
    GameObject StatusBar;
    public float currentFilledPercentage;
    // Start is called before the first frame update
    void Start()
    {
        StatusBar = GameObject.Find("StatusBarCircle");
        currentFilledPercentage = 1f;
        UpdateFilledAmount(currentFilledPercentage);
    }

    public void UpdateFilledAmount(float value)
    {
        if(value < 0f || value > 1f)
        {
            return;
        }
        StatusBar.transform.Find("Fill").GetComponent<Image>().fillAmount = value;

        StatusBar.transform.Find("TextValue").GetComponent<TextMeshProUGUI>().text = ((int)(value*100)).ToString() + "%";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFilledAmount(currentFilledPercentage);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBarScript : MonoBehaviour
{
    GameObject StatusBar;
    public float currentFilledPercentage;
    public Color healthyColor;
    public Color dangerColor;

    void Start()
    {
        StatusBar = GameObject.Find("StatusBarCircle");
        currentFilledPercentage = 1f;
        UpdateFilledAmount(currentFilledPercentage);
    }

    public void UpdateFilledAmount(float value)
    {
        if(value < 0f )
        {
            return;
        }
        if (value > 1)
        {
            value = 1;
        }

        currentFilledPercentage = value;

        UpdateStatusBarColor();

        StatusBar.transform.Find("Fill").GetComponent<Image>().fillAmount = currentFilledPercentage;

        StatusBar.transform.Find("TextValue").GetComponent<TextMeshProUGUI>().text = ((int)(currentFilledPercentage * 100)).ToString() + "%";
    }

    public void UpdateStatusBarColor()
    {
        Color color = healthyColor;

        if (currentFilledPercentage < 0.21f)
        {
            color = dangerColor;
        }
        GameObject.Find("UI").GetComponent<Image>().color = color;
        StatusBar.transform.Find("Fill").GetComponent<Image>().color = color;
        StatusBar.transform.Find("TextValue").GetComponent<TextMeshProUGUI>().color = color;
        StatusBar.transform.Find("TextValue").GetComponent<TextMeshProUGUI>().outlineColor = color == healthyColor ? new Color (159, 234,227) : color;

    }

}

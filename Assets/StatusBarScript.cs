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
        bool isHealthy = true;

        if (currentFilledPercentage < 0.21f)
        {
            isHealthy = false;
        }
        GameObject.Find("UI").GetComponent<Image>().color = isHealthy? healthyColor: dangerColor;
        StatusBar.GetComponent<Image>().color = isHealthy ? new Color(11f/255f, 3f/255f, 94f/255f) : new Color(0, 0, 0);
        StatusBar.transform.Find("Fill").GetComponent<Image>().color = isHealthy ? healthyColor : dangerColor;
        StatusBar.transform.Find("TextValue").GetComponent<TextMeshProUGUI>().color = isHealthy ? healthyColor : dangerColor;
        StatusBar.transform.Find("TextValue").GetComponent<TextMeshProUGUI>().outlineColor = isHealthy ? new Color (159f/255f, 234f/255f,227f/255f) : dangerColor;

    }

}

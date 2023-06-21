using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWhenIdle : MonoBehaviour
{
    public float timeToShow;
    public float minVelocity;
    public CharacterController chara;
    private TMPro.TMP_Text text;
    private Image img;
    private float timeIdle = 0f;

    private void Start()
    {
        img = GetComponent<Image>();
        text = GetComponentInChildren<TMPro.TMP_Text>();
    }

    void Update()
    {
        if (chara.velocity.magnitude < 0.5f)
            timeIdle += Time.deltaTime;
        else
            timeIdle = 0f;
        img.color = new Color(img.color.r, img.color.g, img.color.b, timeIdle - timeToShow);
        text.color = new Color(text.color.r, text.color.g, text.color.b, timeIdle - timeToShow);
    }
}

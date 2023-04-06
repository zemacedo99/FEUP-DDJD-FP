using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Oxygen : MonoBehaviour
{
    public float oxygenValue;
    public Slider oxygenSlider;
    public TMP_Text oxygenText;

    private Vector3 lastPosition;
    private float oxygenLost = 2f;

    void Start()
    {
        lastPosition = transform.position;
        oxygenSlider.maxValue = oxygenValue;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        oxygenValue -= distanceMoved * oxygenLost;
        lastPosition = transform.position;

        if (oxygenValue <= 0)
        {
            oxygenValue = 0;
            Die();
        }
       
        oxygenSlider.value = oxygenValue;
        oxygenText.text = "Oxygen: " + oxygenValue.ToString("F0");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.CompareTag("OxygenStation"))
        {
            RefillOxygen();
        }
    }

    void RefillOxygen()
    {
        oxygenValue = oxygenSlider.maxValue;
        Debug.Log("Oxygen refilled!");
    } 


    void Die()
    {
        // Implement restart the level
        Debug.Log("Player has run out of oxygen");
    }

}

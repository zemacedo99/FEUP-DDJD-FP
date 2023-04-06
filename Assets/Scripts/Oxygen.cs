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
    private Vector3 originalPosition;
    private float oxygenLost = 2f;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = transform.position;
        originalPosition = transform.position;
        Debug.Log("Original position: " + originalPosition);
        oxygenSlider.maxValue = oxygenValue;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        if (distanceMoved < 1)
        {
            oxygenValue -= distanceMoved * oxygenLost;
        }

        if (oxygenValue <= 0)
        {
            oxygenValue = 0;
            Die();
        }
       
        UpdateSlider(oxygenValue);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.CompareTag("OxygenStation"))
        {
            RefillOxygen();
        }
    }

    void UpdateSlider(float value)
    {
        oxygenSlider.value = value;
        oxygenText.text = "Oxygen: " + value.ToString("F0");
    }

    void RefillOxygen()
    {
        oxygenValue = oxygenSlider.maxValue;
        Debug.Log("Oxygen refilled!");
    } 


    void Die()
    {
        Debug.Log("Player has run out of oxygen");
        // Implement restart the level
        controller.enabled = false;
        transform.position = originalPosition;
        controller.enabled = true;
        oxygenValue = oxygenSlider.maxValue;
        UpdateSlider(oxygenValue);
    }

}

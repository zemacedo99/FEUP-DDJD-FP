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
        Debug.Log("Player has run out of oxygen");
        // Implement restart the level
        controller.enabled = false;
        transform.position = originalPosition;
        controller.enabled = true;
        Debug.Log("Transform position: " + transform.position);
        Debug.Log("Original position: " + originalPosition);
        oxygenValue = oxygenSlider.maxValue;

    }

}

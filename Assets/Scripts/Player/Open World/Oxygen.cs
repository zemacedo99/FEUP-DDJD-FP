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
    private Vector3 oxygenStationPosition;
    private float oxygenLostSpeed = 2f;
    private float oxygenRefillSpeed = 50f;
    private bool refilling = false;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = transform.position;
        oxygenStationPosition = transform.position;
        oxygenSlider.maxValue = 300;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        if (distanceMoved < 1 && !refilling)
        {
            oxygenValue -= distanceMoved * oxygenLostSpeed;
        }

        if (oxygenValue <= 0)
        {
            oxygenValue = 0;
            Die();
        }
       
        UpdateSlider(oxygenValue);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("OxygenStation"))
        {
            oxygenStationPosition = other.transform.position;
            StoreCheckpoint();
            RefillOxygen(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("OxygenStation"))
        {
            refilling = false;
            //Debug.Log("Stopped refilling oxygen.");
        }
    }

    void UpdateSlider(float value)
    {
        value = Mathf.Clamp(value, 0f, oxygenSlider.maxValue);
        oxygenSlider.value = value;
        oxygenText.text = "Oxygen: " + value.ToString("F0");
    }

    public void SetOxygenValue(float oxygenValue)
    {
        this.oxygenValue = oxygenValue;
        this.UpdateSlider(oxygenValue);
    }

    void RefillOxygen()
    {
        if(oxygenValue < oxygenSlider.maxValue)
        {
            refilling = true;
            oxygenValue += oxygenRefillSpeed * Time.deltaTime;
            UpdateSlider(oxygenValue);
        }
    }


    void Die()
    {
        Debug.Log("Player has run out of oxygen");
        // Implement restart the level
        controller.enabled = false;
        transform.position = oxygenStationPosition;
        controller.enabled = true;
        oxygenValue = oxygenSlider.maxValue;
        UpdateSlider(oxygenValue);
    }

    void StoreCheckpoint()
    {
        print("Storing");
        PlayerPrefs.SetFloat("CheckpointX", oxygenStationPosition.x);
        PlayerPrefs.SetFloat("CheckpointY", oxygenStationPosition.y);
        PlayerPrefs.SetFloat("CheckpointZ", oxygenStationPosition.z);
    }

    public void LoadCheckpoint()
    {
        if (!PlayerPrefs.HasKey("CheckpointX"))
            return;
        oxygenStationPosition = new Vector3(PlayerPrefs.GetFloat("CheckpointX"), PlayerPrefs.GetFloat("CheckpointY"), PlayerPrefs.GetFloat("CheckpointZ"));
        Die();
    }

}

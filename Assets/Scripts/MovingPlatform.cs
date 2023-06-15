using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 targetA, targetB;
    [SerializeField] private float speed;
    [SerializeField] private bool switching = false;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (!switching)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialPosition + targetB, speed * Time.deltaTime);
        }
        else if (switching)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialPosition + targetA, speed * Time.deltaTime);
        }
        if (transform.localPosition == initialPosition + targetB)
        {
            switching = true;
        }
        else if (transform.localPosition == initialPosition + targetA)
        {
            switching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }


}

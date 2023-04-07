using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 targetA, targetB;
    [SerializeField] private float speed;
    private bool switching = false;
    void Update()
    {
        if (!switching)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetB, speed * Time.deltaTime);
        }
        else if (switching)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetA, speed * Time.deltaTime);
        }
        if (transform.localPosition == targetB)
        {
            switching = true;
        }
        else if (transform.localPosition == targetA)
        {
            switching = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;
    public bool uniqueItem = true;
    public float degreesPerSecond = 45.0f;
    public float amplitude = 0.25f; // amplitude of the up and down movement
    public float frequency = 1f; // frequency of the up and down movement

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        if (uniqueItem && FindObjectOfType<PlayerInventory>().HasItem(item))
            Destroy(this.gameObject);
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}


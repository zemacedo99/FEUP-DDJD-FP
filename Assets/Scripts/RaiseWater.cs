using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseWater : MonoBehaviour
{

    [SerializeField] private GameObject water;
    private float waterLevel;
    public float speed;

    public void Drop(bool raise)
    {
        waterLevel = water.transform.position.y;
        water.transform.position = new Vector3(water.transform.position.x, waterLevel - 10, water.transform.position.z);
        enabled = raise;
        if(raise)
        {
            print("raising water");
            FindFirstObjectByType<PlayerMovement>().gameObject.transform.position = new Vector3(water.transform.position.x, waterLevel - 9, water.transform.position.z);
        }
    }

    void Update()
    {
        water.transform.position += new Vector3(0, Time.deltaTime* speed, 0);
        if(water.transform.position.y >= waterLevel)
        {
            water.transform.position = new Vector3(water.transform.position.x, waterLevel, water.transform.position.z);
            this.enabled = false;
        }
    }
}

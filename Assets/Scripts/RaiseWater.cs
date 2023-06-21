using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseWater : MonoBehaviour
{

    [SerializeField] private GameObject water;
    private float waterLevel;
    public float speed;

    public Vector3 Drop(bool raise)
    {
        waterLevel = water.transform.position.y;
        water.transform.position = new Vector3(water.transform.position.x, waterLevel - 7, water.transform.position.z);
        enabled = raise;
        if(raise)
        {
            print("raising water");
            GameObject player = FindFirstObjectByType<PlayerMovement>().gameObject;
            var height = player.GetComponent<CharacterController>().bounds.extents.y;
            return new Vector3(water.transform.position.x, water.gameObject.transform.GetChild(0).position.y+height+0.6f, water.transform.position.z);
        }
        return new Vector3();
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

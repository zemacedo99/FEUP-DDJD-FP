using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceDetectorScript : MonoBehaviour
{
    public GameObject door;
    DoorsScript doorsScript;

    // Start is called before the first frame update
    void Start()
    {
        doorsScript = door.GetComponent<DoorsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        doorsScript.Open();
    }

    private void OnTriggerExit(Collider other)
    {
        doorsScript.Close();
        
    }
}

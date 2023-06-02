using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceDetectorScript : MonoBehaviour
{
    public int minimumPresences = 1;
    public GameObject door;
    DoorsScript doorsScript;
    List<Collider> colliders;

    // Start is called before the first frame update
    void Start()
    {
        doorsScript = door.GetComponent<DoorsScript>();

        colliders = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Remove null entries
        for (int i = 0; i < colliders.Count; ++i)
        {
            if (!colliders[i])
            {
                // Remove colliders that were destroyed
                colliders.RemoveAt(i);
            }

        }

        if (colliders.Count < minimumPresences)
            doorsScript.Close();
    }

    private void OnTriggerEnter(Collider other)
    {
        colliders.Add(other);

        if (colliders.Count >= minimumPresences)
        {
            Debug.Log("Opening " + door.name);
            doorsScript.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}

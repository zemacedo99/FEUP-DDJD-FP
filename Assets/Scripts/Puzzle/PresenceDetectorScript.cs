using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceDetectorScript : MonoBehaviour
{
    public int doorMinimumPresences = 1;
    public GameObject door;
    DoorsScript doorsScript;

    public int movingPlatformMinimumPresences = 2;
    public MovingPlatform movingPlatform;

    List<Collider> colliders;

    // Start is called before the first frame update
    void Start()
    {
        colliders = new List<Collider>();

        if (door)
            doorsScript = door.GetComponent<DoorsScript>();
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

        if (doorsScript && colliders.Count < doorMinimumPresences)
            doorsScript.Close();
        if (movingPlatform && colliders.Count < movingPlatformMinimumPresences)
            movingPlatform.moving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        colliders.Add(other);

        if (doorsScript && colliders.Count >= doorMinimumPresences)
        {
            doorsScript.Open();
        }
        if (movingPlatform && colliders.Count >= movingPlatformMinimumPresences)
        {
            movingPlatform.moving = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}

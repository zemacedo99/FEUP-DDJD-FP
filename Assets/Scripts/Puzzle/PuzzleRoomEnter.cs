using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoomEnter : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        door.GetComponent<DoorsScript>().Close();
    }
}

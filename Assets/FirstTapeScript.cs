using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTapeScript : MonoBehaviour
{
    void Start()
    {
        if(PlayerPrefs.GetInt("IsFirstTapeCollected") == 1 || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().HasItem("Strange Tape I"))
        {
            Destroy(gameObject);
        }
    }

}

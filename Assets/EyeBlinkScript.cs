using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinkScript : MonoBehaviour
{
    GameObject topEyeLidObject;
    GameObject botEyeLidObject;

    // Start is called before the first frame update
    void Start()
    {
        topEyeLidObject = GameObject.Find("TopEyeLid");
        botEyeLidObject = GameObject.Find("BotEyeLid");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

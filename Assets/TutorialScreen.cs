using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TutorialScreen : MonoBehaviour
{
    public List<TutorialPages> tutorials = new List<TutorialPages>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}

[Serializable]
public class TutorialPages
{ 

    public string tutorialPageName;
    public List<TutorialObject> tutorials = new List<TutorialObject>();

}

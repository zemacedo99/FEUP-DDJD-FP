using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuScript : MonoBehaviour
{
    private int selectedOption;
    public string missionDescription;

    void Start()
    {
        selectedOption = 0;
        missionDescription = "Escape the planet\nCollect Spaceship Pieces(0 / 6)";
    }
 
}

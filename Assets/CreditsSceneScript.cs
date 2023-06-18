using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class CreditsSceneScript : MonoBehaviour
{
    public float duration = 25;

    public InputAction selectInput;
    public InputActionAsset actions;

    void Start()
    {
        actions.FindActionMap("menu interactions").Enable();
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("select", true);
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }
        if (selectInput.WasPressedThisFrame())
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }
    }
}

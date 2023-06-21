using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class IntroScript : MonoBehaviour
{
    public PlayableDirector currentTimeline;

    public InputAction selectInput;
    public InputActionAsset actions;
    // Start is called before the first frame update
    void Start()
    {
        actions.FindActionMap("menu interactions").Enable();
        selectInput = actions.FindActionMap("menu interactions", true).FindAction("skip", true);

    }

    private void SkipToLastTextScene()
    {

        for (int i = 1; i < transform.childCount - 1; i++)
        {
            Destroy(transform.GetChild(i).gameObject);

        }
        currentTimeline.time = currentTimeline.duration - 9;

    }

    // Update is called once per frame
    void Update()
    {
        if (selectInput.WasPressedThisFrame())
        {
            SkipToLastTextScene();
        }
    }
}

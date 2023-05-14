using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsScript : MonoBehaviour
{
    public GameObject doorFront;
    public GameObject doorBack;

    public Transform audioSourceTransform;
    public FMODUnity.EventReference doorClosingEvent;
    public FMODUnity.EventReference doorOpeningEvent;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) Open();
        if (Input.GetKeyDown(KeyCode.C)) Close();
    }

    public void Open()
    {
        if (doorFront.activeInHierarchy || doorBack.activeInHierarchy)
            FMODUnity.RuntimeManager.PlayOneShot(doorOpeningEvent, audioSourceTransform.position);
        if (doorFront.activeInHierarchy)
            doorFront.SetActive(false);
        if (doorBack.activeInHierarchy)
            doorBack.SetActive(false);
    }
    public void Close()
    {
        if (!doorFront.activeInHierarchy || !doorBack.activeInHierarchy)
            FMODUnity.RuntimeManager.PlayOneShot(doorClosingEvent, audioSourceTransform.position);
        if (!doorFront.activeInHierarchy)
            doorFront.SetActive(true);
        if (!doorBack.activeInHierarchy)
            doorBack.SetActive(true);
    }

}

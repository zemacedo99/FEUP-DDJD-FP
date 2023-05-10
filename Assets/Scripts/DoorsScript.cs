using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsScript : MonoBehaviour
{
    public GameObject doorFront;
    public GameObject doorBack;

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
        if (doorFront.activeInHierarchy)
            doorFront.SetActive(false);
        if (doorBack.activeInHierarchy)
            doorBack.SetActive(false);
    }
    public void Close()
    {
        if (!doorFront.activeInHierarchy)
            doorFront.SetActive(true);
        if (!doorBack.activeInHierarchy)
            doorBack.SetActive(true);
    }

}

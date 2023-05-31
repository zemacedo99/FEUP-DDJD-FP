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

    public List<ItemObject> verificationItemList;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OpenDoorIfHasAlItems()
    {
        if (verificationItemList.Count == 0) return;
        for (int i = 0; i < verificationItemList.Count; i++)
        {
            if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().HasItem(verificationItemList[i]))
            {
                return;
            }
        }
        Open();
        return;
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

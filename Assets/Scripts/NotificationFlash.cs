using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationFlash : MonoBehaviour
{
    public int flashMax = 7;
    private int flashCount = 0;
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void Enable()
    {
        InvokeRepeating(nameof(FlashNotif), 3f, 0.5f);
        this.gameObject.SetActive(true);
        flashCount = 0;
    }

    void FlashNotif()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        flashCount++;
        if (flashCount == flashMax)
            CancelInvoke(nameof(FlashNotif));
    }
}

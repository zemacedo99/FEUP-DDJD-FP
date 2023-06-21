using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public RawImage img;
    public float speedX, speedY;

    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(speedX, speedY) * Time.deltaTime, img.uvRect.size);
    }
}

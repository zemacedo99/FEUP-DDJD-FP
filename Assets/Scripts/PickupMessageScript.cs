using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PickupMessageScript : MonoBehaviour
{
    public RawImage itemImage;
    public TextMeshProUGUI pickupText;

    public void UpdateMessage(Item item)
    {
        SetItemImage(item);
        SetText(item);
    }

    private void SetText(Item item)
    {
        pickupText.text = item.item.itemName;
    }
    private void SetItemImage(Item item)
    {
        itemImage.texture = item.item.prefab.GetComponent<RawImage>().texture;
    }
}

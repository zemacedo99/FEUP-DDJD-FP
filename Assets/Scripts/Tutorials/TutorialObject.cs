using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class TutorialObject : ScriptableObject
{
    public Sprite image;
    [TextArea(2, 20)]
    public string title;
    [TextArea(2, 20)]
    public string description;
}


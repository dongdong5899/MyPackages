using System;
using UnityEngine;

[Serializable]
public abstract class TextEvent
{
    public int eventIndex;

    public abstract void OnEvent();
}

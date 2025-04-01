using System;
using TMPro;
using UnityEngine;

[Serializable]
public class TextEffect
{
    [SerializeField] protected Vector2Int _applyArea;
    protected TMP_Text _tmpText;
    public virtual void Init(TMP_Text tmpText, Color32[] vertexColors)
    {
        _tmpText = tmpText;
    }
    public virtual void TextVisible(int index)
    {

    }
    public virtual void EffectUpdate(Color32[] vertexColors)
    {

    }
}

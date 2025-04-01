using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TextEffectData
{
    public string effectName;
    [SerializeReference] public TextEffect textEffect;
}


[Serializable]
public class TextEventData
{
    public string eventName;
    [SerializeReference] public TextEvent textEvent;
}

[Serializable]
public class TextNextConditionData
{
    public string conditionName;
    [SerializeReference] public TextNextCondition textNextCondition;
}

[Serializable]
public class DialogueTextData
{
    public string text;
    public TalkerSO talkerSO;
    public float textSpeed = 0.05f;
    public Stack<float> textSpeedOverlap = new Stack<float>();

    public List<TextEffectData> textEffectList;
    public List<TextEventData> textEventList;
    public TextNextConditionData textNextConditionData;

    public float GetTextSpeed()
    {
        return textSpeedOverlap.Count == 0 ? textSpeed : textSpeedOverlap.Peek();
    }
}

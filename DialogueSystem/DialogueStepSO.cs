using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueStepSO", menuName = "SO/DialogueStep")]
public class DialogueStepSO : ScriptableObject
{
    public List<DialogueTextData> dialogueContextList;

    private void OnValidate()
    {
        dialogueContextList.ForEach(textData =>
        {
            foreach (TextEffectData effectData in textData.textEffectList)
            {
                string effectName = $"Text{effectData.effectName}Effect";
                if (effectData.textEffect != null && effectData.textEffect.GetType().Name == effectName)
                    continue;
                else
                    effectData.textEffect = null;
                try
                {
                    Type type = Type.GetType(effectName);

                    TextEffect textEffect = Activator.CreateInstance(type) as TextEffect;
                    effectData.textEffect = textEffect;
                }
                catch (Exception e)
                {
                    // 디버그 안뜨게하고싶음
                    if (e == null)
                        Debug.Log(e.ToString());
                }
            }
            foreach (TextEventData eventData in textData.textEventList)
            {
                string eventName = $"{eventData.eventName}TextEvent";
                if (eventData.textEvent != null && eventData.textEvent.GetType().Name == eventName)
                    continue;
                else
                    eventData.textEvent = null;
                try
                {
                    Type type = Type.GetType(eventName);

                    TextEvent textEvent = Activator.CreateInstance(type) as TextEvent;
                    eventData.textEvent = textEvent;
                }
                catch (Exception e)
                {
                    // 디버그 안뜨게하고싶음
                    if (e == null)
                        Debug.Log(e.ToString());
                }
            }

            TextNextConditionData textNextCondition = textData.textNextConditionData;
            string nextConditionName = $"Text{textNextCondition.conditionName}NextCondition";
            if (textNextCondition.textNextCondition == null || textNextCondition.textNextCondition.GetType().Name != nextConditionName)
            {
                try
                {
                    Type type = Type.GetType(nextConditionName);

                    TextNextCondition nextCondition = Activator.CreateInstance(type) as TextNextCondition;
                    textNextCondition.textNextCondition = nextCondition;
                }
                catch (Exception e)
                {
                    // 디버그 안뜨게하고싶음
                    textNextCondition.textNextCondition = null;
                    if (e == null)
                        Debug.Log(e.ToString());
                }
            }
        });
    }
}

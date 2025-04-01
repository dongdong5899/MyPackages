using UnityEngine;

public class EnterStepTextEvent : TextEvent
{
    [SerializeField] private DialogueStepSO _stepSO;

    public override void OnEvent()
    {
        DialogueStepPlayer stepPlayer = new DialogueStepPlayer(_stepSO);
        DialogueManager.Instance.EnterStep(stepPlayer);
    }
}

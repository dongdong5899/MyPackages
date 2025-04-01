using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    private Dictionary<TalkerSO, Talker> _talkerDictionary = new();
    private List<DialogueStepPlayer> _stepList = new();

    [SerializeField] private DialogueStepSO DialogueStepSO;

    private bool _isInitiolized = false;

    protected override void OnCreateInstance()
    {
        base.OnCreateInstance();
        Init();
    }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        int stepCounr = _stepList.Count;
        for (int i = 0; i < stepCounr; i++)
        {
            if (_stepList.Count < stepCounr) break;
            _stepList[i].Update();
        }
    }

    private void Init()
    {
        if (_isInitiolized) return;
        _isInitiolized = true;
        Talker[] talkers = FindObjectsByType<Talker>(FindObjectsSortMode.None);
        foreach (Talker talker in talkers)
        {
            AddTalker(talker);
        }

        DialogueStepPlayer stepPlayer = new DialogueStepPlayer(DialogueStepSO);
        EnterStep(stepPlayer);
    }

    public void EnterStep(DialogueStepPlayer stepPlayer)
    {
        _stepList.Add(stepPlayer);
    }

    public void ExitStep(DialogueStepPlayer stepPlayer)
    {
        _stepList.Remove(stepPlayer);
    }

    private void AddTalker(Talker talker)
    {
        if (_talkerDictionary.ContainsKey(talker.TalkerSO) == false)
            _talkerDictionary.Add(talker.TalkerSO, talker);
        else
        {
            Debug.LogWarning($"'{talker.TalkerSO.talkerName}'은 이미 추가된 Talker입니다.");
        }
    }

    public Talker GetTalker(TalkerSO talkerSO)
    {
        return _talkerDictionary[talkerSO];
    }
}

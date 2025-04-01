using TMPro;
using UnityEngine;

public class DialogueStepPlayer
{
    private DialogueStepSO _stepSO;
    public int index = 0;

    public DialogueTextData _currentTextData;
    public Talker _currentTalker;
    public TMP_Text _tmpText;

    private int _textIndex;
    private float _lastTextAppendTime;
    private Color32[] _vertexColors;
    private Color32[] _currentVertexColors;

    public DialogueStepPlayer(DialogueStepSO stepSO)
    {
        _stepSO = stepSO;
        index = 0;

        PlayStep(false);
    }

    public void Update()
    {
        if (_currentTextData != null)
        {
            int effectCount = _currentTextData.textEffectList.Count;
            for (int i = 0; i < effectCount; i++)
            {
                if (_currentTextData.textEffectList.Count < effectCount) break;
                _currentTextData.textEffectList[i].textEffect.EffectUpdate(_vertexColors);
            }
            int eventCount = _currentTextData.textEventList.Count;
            if (_textIndex < _tmpText.textInfo.characterCount)
            {
                if (_lastTextAppendTime + _currentTextData.textSpeed < Time.time)
                {
                    _lastTextAppendTime = Time.time;

                    for (int i = 0; i <= _textIndex; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            _currentVertexColors[i * 4 + j] = _vertexColors[i * 4 + j];
                        }
                    }

                    _tmpText.textInfo.meshInfo[0].colors32 = _currentVertexColors;

                    for (int i = 0; i < effectCount; i++)
                    {
                        if (_currentTextData.textEffectList.Count < effectCount) break;
                        _currentTextData.textEffectList[i].textEffect.TextVisible(_textIndex);
                    }

                    for (int i = 0; i < eventCount; i++)
                    {
                        if (_currentTextData.textEventList.Count < eventCount) break;
                        TextEvent textEvent = _currentTextData.textEventList[i].textEvent;
                        if (_textIndex == textEvent.eventIndex)
                            _currentTextData.textEventList[i].textEvent.OnEvent();
                    }

                    _textIndex++;
                }
                _currentTextData.textNextConditionData.textNextCondition.OnUpdate(false);
            }
            else
            {
                _currentTextData.textNextConditionData.textNextCondition.OnUpdate(true);
                _tmpText.textInfo.meshInfo[0].colors32 = _vertexColors;
            }

            _tmpText.UpdateVertexData();
        }
    }

    public void PlayStep(bool nextIndex = true)
    {
        if (nextIndex) index++;
        _currentTalker?.ClearText();

        if (index >= _stepSO.dialogueContextList.Count)
        {
            _currentTextData = null;
            DialogueManager.Instance.ExitStep(this);
            return;
        }

        _currentTextData = _stepSO.dialogueContextList[index];
        TalkerSO talkerSO = _currentTextData.talkerSO;
        Talker talker = DialogueManager.Instance.GetTalker(talkerSO);

        _currentTalker = talker;
        _tmpText = _currentTalker.PrintText(_currentTextData.text);
        TMPSetting();
        _currentTextData.textNextConditionData.textNextCondition.TextNextEvent += GetHandleTextNextEvent;
    }

    private void TMPSetting()
    {
        // 초기값 설정
        _textIndex = 0;
        _lastTextAppendTime = 0;

        int colorCount = _tmpText.textInfo.meshInfo[0].colors32.Length;

        // 기본색 및 투명색 생성
        _currentVertexColors = new Color32[colorCount];
        _vertexColors = new Color32[colorCount];
        for (int i = 0; i < colorCount; i++)
        {
            _vertexColors[i] = _tmpText.textInfo.meshInfo[0].colors32[i];
            _currentVertexColors[i] = new Color32(0, 0, 0, 0);
        }

        // 모듈들의 Init실행
        for (int i = 0; i < _currentTextData.textEffectList.Count; i++)
        {
            _currentTextData.textEffectList[i].textEffect.Init(_tmpText, _vertexColors);
        }
        _currentTextData.textNextConditionData.textNextCondition.Init();

        // 색갈 끄기
        Color32[] colorInfo = _tmpText.textInfo.meshInfo[0].colors32;
        for (int i = 0; i < colorCount; i++)
        {
            colorInfo[i] = _currentVertexColors[i];
        }
        _tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    private void GetHandleTextNextEvent()
    {
        _currentTextData.textNextConditionData.textNextCondition.TextNextEvent -= GetHandleTextNextEvent;
        PlayStep();
    }
}

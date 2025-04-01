using System;
using TMPro;
using UnityEngine;

[Serializable]
public class TextChangeColorEffect : TextEffect
{
    [SerializeField] private Gradient _color;
    [SerializeField] private float _duration;
    [SerializeField] private bool _loop;

    private float _startTime;

    public override void Init(TMP_Text tmpText, Color32[] vertexColors)
    {
        base.Init(tmpText, vertexColors);

        _startTime = Time.time;
    }

    public override void EffectUpdate(Color32[] vertexColors)
    {
        base.EffectUpdate(vertexColors);

        float amount = Time.time - _startTime;
        if (amount < _duration)
        {
            for (int i = _applyArea.x; i < _applyArea.y; i++)
            {
                Color32 color32 = _color.Evaluate(amount / _duration);
                for (int j = 0; j < 4; j++)
                {
                    vertexColors[i * 4 + j] = color32;
                }
            }
        }
        else if (_loop)
            _startTime = Time.time;
    }
}

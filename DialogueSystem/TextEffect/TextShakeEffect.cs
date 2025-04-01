using System.Drawing;
using TMPro;
using UnityEngine;

public class TextShakeEffect : TextEffect
{
    [SerializeField] private float _shakePower = 1;
    [SerializeField] private float _shakeSpeed = 10;
    private Vector3[] _vertexPos;

    private float _lastShakeTime;

    public override void Init(TMP_Text tmpText, Color32[] vertexColors)
    {
        base.Init(tmpText, vertexColors);

        Vector3[] vertexPos = _tmpText.textInfo.meshInfo[0].vertices;
        int count = vertexPos.Length;
        _vertexPos = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            _vertexPos[i] = vertexPos[i];
        }
        _lastShakeTime = Time.time;
    }

    public override void EffectUpdate(Color32[] vertexColors)
    {
        base.EffectUpdate(vertexColors);

        if (_lastShakeTime + 1f / _shakeSpeed < Time.time)
        {
            _lastShakeTime = Time.time;
            Vector3[] vertexPos = _tmpText.textInfo.meshInfo[0].vertices;
            for (int i = _applyArea.x; i < _applyArea.y; i++)
            {
                Vector3 insideUnitCircle = Random.insideUnitCircle * (_shakePower * _tmpText.fontSize / 500);
                for (int j = 0; j < 4; j++)
                {
                    vertexPos[i * 4 + j] = _vertexPos[i * 4 + j] + insideUnitCircle;
                }
            }
        }
    }
}

using TMPro;
using UnityEngine;

public class TextColorEffect : TextEffect
{
    [SerializeField] private Color _color;

    public override void Init(TMP_Text tmpText, Color32[] vertexColors)
    {
        base.Init(tmpText, vertexColors);
        
        for (int i = _applyArea.x; i < _applyArea.y; i++)
        {
            Color32 color32 = _color;
            for (int j = 0; j < 4; j++)
            {
                vertexColors[i * 4 + j] = color32;
            }
        }
    }
}

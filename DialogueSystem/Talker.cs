using System;
using System.Drawing;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class Talker : MonoBehaviour
{
    [field: SerializeField] public TalkerSO TalkerSO { get; private set; }

    [SerializeField] public TMP_Text _tmpText;
    [SerializeField] public Transform _textBox;

    private void Awake()
    {
        ClearText();
    }

    public TMP_Text PrintText(string text)
    {
        _tmpText.text = text;
        _tmpText.GraphicUpdateComplete();
        _tmpText.ForceMeshUpdate();

        Bounds bounds = _tmpText.textBounds;

        _textBox.localScale = new Vector3(bounds.size.x + 0.2f, bounds.size.y + 0.2f, 1);

        return _tmpText;
    }

    internal void ClearText()
    {
        _tmpText.text = "";
        _textBox.localScale = Vector3.zero;
    }
}

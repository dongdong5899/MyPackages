using UnityEngine;
using UnityEngine.InputSystem;

public class TextInputNextCondition : TextNextCondition
{
    [SerializeField] private float _canNextDelay;
    [SerializeField] private bool _onPlayPrintCompleted = false;

    private float _currentTime;

    public override void Init()
    {
        _currentTime = 0;
    }

    public override void OnUpdate(bool isEnded)
    {
        if (_onPlayPrintCompleted && isEnded == false) return;

        _currentTime += Time.deltaTime;

        if (_canNextDelay < _currentTime)
        {
            if (Keyboard.current.spaceKey.isPressed || 
                Keyboard.current.enterKey.isPressed)
            {
                TextNextEvent?.Invoke();
            }
        }
    }
}

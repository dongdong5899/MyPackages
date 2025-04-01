using UnityEngine;

public class TextDelayNextCondition : TextNextCondition
{
    [SerializeField] private float _delay;
    [SerializeField] private bool _onPlayPrintCompleted = true;

    private float _currentTime;

    public override void Init()
    {
        _currentTime = 0;
    }

    public override void OnUpdate(bool isEnded)
    {
        if (_onPlayPrintCompleted && isEnded == false) return;

        _currentTime += Time.deltaTime;

        if (_delay < _currentTime)
        {
            TextNextEvent?.Invoke();
        }
    }
}

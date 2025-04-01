using System;

[Serializable]
public abstract class TextNextCondition
{
    public abstract void Init();
    public abstract void OnUpdate(bool isEnded);
    public Action TextNextEvent;
}
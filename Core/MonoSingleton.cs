using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindFirstObjectByType<T>();
                _Instance.OnCreateInstance();
            }
            return _Instance;
        }
    }

    /// <summary>
    /// 인스턴스 생성 당시 한번만 실행됩니다.
    /// </summary>
    protected virtual void OnCreateInstance() { }
}

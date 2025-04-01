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
    /// �ν��Ͻ� ���� ��� �ѹ��� ����˴ϴ�.
    /// </summary>
    protected virtual void OnCreateInstance() { }
}

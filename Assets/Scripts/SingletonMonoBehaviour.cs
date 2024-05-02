using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    private static readonly object _lock = new object();
    private static bool _shuttingDown = false;

    public static T Instance
    {
        get
        {
            if (_shuttingDown == true)
            {
                _instance = null;
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindAnyObjectByType(typeof(T));
                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = "Singleton_ " + typeof(T);

                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
            
        }
    }

    protected virtual void OnApplicationQuit()
    {
        _shuttingDown = true;
        _instance = null;
    }

    protected virtual void Awake()
    {
        _shuttingDown = false;
    }
}

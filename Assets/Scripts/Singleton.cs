using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {

    protected static Singleton<T> mInstance
    {
        get
        {
            if (!_mInstance)
            {
                T[] managers = FindObjectsOfType(typeof(T)) as T[];
                if (managers.Length != 0)
                {
                    if (managers.Length == 1)
                    {
                        _mInstance = managers[0];
                        _mInstance.gameObject.name = typeof(T).Name;
                        return _mInstance;
                    }
                    else
                    {
                        Debug.LogError("You have more than one " + typeof(T).Name + " in the scene. You only need 1, it's a singleton!");
                        foreach (T manager in managers)
                        {
                            Destroy(manager.gameObject);
                        }
                    }
                }
                GameObject go = new GameObject(typeof(T).Name, typeof(T));
                _mInstance = go.GetComponent<T>();
                DontDestroyOnLoad(go);
            }
            return _mInstance;
        }
        set 
        {
           _mInstance = value as T;
        }
    }
    private static T _mInstance;
}

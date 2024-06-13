using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
    protected bool m_bInit;
    public static T instance
    {
        get
        {
            if (_instance == false)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }

            if (_instance == false)
            {
                GameObject _gameObject = new GameObject();
                _gameObject.name = typeof(T).Name;

                _instance = _gameObject.AddComponent<T>();
                DontDestroyOnLoad(_gameObject);
            }

            return _instance;
        }
    }
}

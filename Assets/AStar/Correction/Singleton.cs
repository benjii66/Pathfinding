using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    static T instance = default;
    public static T Instance => instance;
    public virtual bool IsValid => instance;


    protected virtual void Awake()
    {
        if (Instance != null && this != instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this as T;
        name += $" [{typeof(T).Name}]";
    }

 
}

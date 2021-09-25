using UnityEngine;

public class Manager<T> : MonoBehaviour where T : Manager<T>
{
    public static T Instance;

    public virtual void Awake()
    {
        Instance = this as T;
    }

    public virtual void OnDestroy()
    {
        Instance = null;
    }
}
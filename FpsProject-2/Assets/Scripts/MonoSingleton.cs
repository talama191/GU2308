
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>, new()
{
    private static T _instance = new T();

    public static T Instance => _instance;

}

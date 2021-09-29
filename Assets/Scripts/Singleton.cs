using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    protected static bool isPersistent = false;

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] singletons = FindObjectsOfType<T>();

                // If there is no singleton of this type, make one
                if (singletons.Length == 0)
                    instance = new GameObject($"{typeof(T).Name}-Singleton").AddComponent<T>();
                // If there is one, make the instance to the first one
                if (singletons.Length > 0)
                    instance = singletons[0];
                // Destroy any singleton that is not set as the instance of this singleton
                if (singletons.Length > 1)
                {
                    foreach (var singleton in singletons)
                    {
                        if (singleton != instance)
                            Destroy(singleton);
                    }
                }

                // If we are persistent, don't destroy the object
                if (isPersistent)
                    DontDestroyOnLoad(instance);
            }

            // Return the instance
            return instance;
        }
    }
}

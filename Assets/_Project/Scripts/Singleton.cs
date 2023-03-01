using UnityEngine;


    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;

        public static T Instance => GetInstance();

        private static T GetInstance()
        {
            if (instance == null) FindInstance();
            return instance;
        }

        private static void FindInstance()
        {
            var singleton = FindObjectOfType<T>();
            
            if (singleton == null)
                Debug.LogError($"No Object With Script: {nameof(T)}");
            else
                instance = singleton;
        }
    }
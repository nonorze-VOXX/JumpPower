using UnityEngine;

namespace DefaultNamespace
{
    public class UnityBridge<T> where T : Object
    {
        private readonly string _path;
        private T _instance;

        public UnityBridge(string path)
        {
            _path = path;
        }

        public T GetInstance()
        {
            if (_instance == null)
            {
                GameObject.Find(_path);
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                    Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
            }

            return _instance;
        }
    }
}
using UnityEngine;

namespace Gamekit2D.Runtime.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static Singleton<T> _instance;

        public static Singleton<T> Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

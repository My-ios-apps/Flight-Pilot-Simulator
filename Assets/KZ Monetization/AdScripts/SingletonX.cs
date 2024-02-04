using UnityEngine;

namespace KZ.Utils
{
    public class SingletonX<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T m_Instance;
        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = (T)FindObjectOfType(typeof(T));

                return m_Instance;
            }
        }
    }
}

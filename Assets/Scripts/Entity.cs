using UnityEngine;

namespace Common
{
    /// <summary>
    /// The base class of all interactive game objects on the scene. 
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// The name of objects for user.
        /// </summary>
        [SerializeField] private string m_Nickname;
        public string Nickname => m_Nickname;
    }

}

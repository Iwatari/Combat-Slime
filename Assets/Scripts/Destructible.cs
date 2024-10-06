using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{

    /// <summary>
    /// The distructible object on the scene. What can have hitpoints.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// The object ignores the damage.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        ///<summary>
        ///The starting number of hitpoints.
        ///</summary>
        [SerializeField] protected int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;
        ///<summary>
        ///The current number of hitpoints.
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;

            transform.SetParent(null);
        }

        #endregion

        #region Public API
        /// <summary>
        /// Applying damage to an object.
        /// </summary>
        /// <param name="damage"> Damage to an object </param>
        public void AplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if(m_CurrentHitPoints <= 0) 
                OnDeath();
        }
        public void SetIndestructible(bool value)
        {
            m_Indestructible = value;
        }

        public void ActivateInvulnerability(float duration)
        {
            SetIndestructible(true);
            Invoke("DeactivateInvulnerability", duration);
        }

        private void DeactivateInvulnerability()
        {
            SetIndestructible(false);
        }

        #endregion
        /// <summary>
        /// Redefined object destruction event when hitpoints are below zero.
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestructible;

        public static IReadOnlyCollection<Destructible> AllDestructible => m_AllDestructible;

        protected virtual void OnEnable()
        {
            
            if (m_AllDestructible == null)
            {
                m_AllDestructible = new HashSet<Destructible>();
            }
            
            m_AllDestructible.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructible.Remove(this);
        }

        public const int TeamIDNeutral = 0;

        [SerializeField] private int m_TeamID;
        public int TeamID => m_TeamID;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;
    }
}

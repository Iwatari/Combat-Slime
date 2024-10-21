using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSlime
{
    public class PowerUpStats : PowerUp
    {
        public enum EffectType
        { 
            AddInvulnerability,
            AddSpeed,
            AddDamage,
            AddHealth
        }

        [SerializeField] private EffectType m_EffectType;

        [SerializeField] private float m_Timer;

        [SerializeField] private float m_Value;

        protected override void OnPickedUp(Slime slime)
        {
            if (m_EffectType == EffectType.AddInvulnerability)
            {
                slime.ActivateInvulnerability((int)m_Timer);
            }

            if (m_EffectType == EffectType.AddSpeed)
            {
                slime.AddSpeed((int)m_Timer, (int)m_Value);
            }
        }

    }
}

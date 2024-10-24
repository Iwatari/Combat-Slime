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
            AddHealth
        }

        [SerializeField] private EffectType m_EffectType;

        [SerializeField] private float m_Timer;

        [SerializeField] private float m_Value;

        protected override void OnPickedUp(Slime slime)
        {
            switch (m_EffectType)
            {
                case EffectType.AddInvulnerability:
                    slime.ActivateInvulnerability((int)m_Timer);
                    break;
                case EffectType.AddSpeed:
                    slime.AddSpeed((int)m_Timer, (int)m_Value);
                    break;
                case EffectType.AddHealth:
                    slime.AddHealth((int)m_Value);
                    break;
            }
        }

    }
}

using UnityEngine;
using UnityEngine.UI;
using Common;

namespace CombatSlime
{
    public class BotsHitpointBar : MonoBehaviour
    {
        [SerializeField] private Image m_Image;
        private Destructible target;

        private float lastHitPoints;

        private void Awake()
        {
            target = GetComponentInParent<Destructible>();
        }

        private void Update()
        {
            if (target == null) return;

            float hitPoints = (float)target.HitPoints / target.MaxHitPoints;

            if (hitPoints != lastHitPoints)
            {
                m_Image.fillAmount = hitPoints;
                lastHitPoints = hitPoints;
            }
        }
    }
}

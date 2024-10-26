using UnityEngine.UI;
using UnityEngine;

namespace CombatSlime
{
    public class HitpointBar : MonoBehaviour
    {
        [SerializeField] private Image m_Image;

        private float lastHitPoints;

        private void Update()
        {
            if (Player.Instance == null || Player.Instance.ActiveSlime == null)
            {
                return; 
            }

            float hitPoints = ((float)Player.Instance.ActiveSlime.HitPoints / (float)Player.Instance.ActiveSlime.MaxHitPoints);

            if (hitPoints != lastHitPoints)
            {
                m_Image.fillAmount = hitPoints;
                lastHitPoints = hitPoints;
            }
        }
    }
}

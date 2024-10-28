using UnityEngine;

namespace CombatSlime
{
    public class AIColorController : MonoBehaviour
    {
        public enum SlimeColor
        {
            White,
            Blue,
            Yellow
        }

        [Header("Настройки цвета")]
        public SlimeColor m_SlimeColor;

        private SpriteRenderer m_SpriteRenderer;

        private void Awake()
        {
            m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SetColor(m_SlimeColor);
        }

        public void SetColor(SlimeColor newColor)
        {
            m_SlimeColor = newColor;
            m_SpriteRenderer.color = GetColor(newColor);
        }

        private Color GetColor(SlimeColor color)
        {
            switch (color)
            {
                case SlimeColor.Blue: return Color.blue;
                case SlimeColor.Yellow: return Color.yellow;
                case SlimeColor.White: return Color.white;
                default: return Color.white;
            }
        }

        public SlimeColor GetCurrentColor()
        {
            return m_SlimeColor;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (m_SpriteRenderer == null) m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SetColor(m_SlimeColor);
        }
#endif
    }
}

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
        public SlimeColor slimeColor;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SetColor(slimeColor);
        }

        public void SetColor(SlimeColor newColor)
        {
            slimeColor = newColor;
            spriteRenderer.color = GetColor(newColor);
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
            return slimeColor;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SetColor(slimeColor);
        }
#endif
    }
}

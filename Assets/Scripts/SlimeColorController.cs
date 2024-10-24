using UnityEngine;

namespace CombatSlime
{
    public class SlimeColorController : MonoBehaviour
    {
        private SpriteRenderer m_Renderer;

        private Color m_WhiteColor = Color.white;
        private Color m_BlueColor = Color.blue;
        private Color m_YellowColor = Color.yellow;
        [SerializeField] private AudioSource m_ColorChangeSound;

        private int m_ColorIndex = 0;

        private Slime m_Slime;

        private void Start()
        {
            m_Renderer = GetComponentInChildren<SpriteRenderer>();
            m_Renderer.color = m_WhiteColor;

            m_Slime = GetComponentInParent<Slime>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (m_ColorIndex == 0)
                {
                    m_ColorChangeSound.Play();
                    SetColorAndWeaponMode(m_BlueColor, WeaponMode.Blue);
                    m_ColorIndex = 1;
                }
                else if (m_ColorIndex == 2)
                {
                    m_ColorChangeSound.Play();
                    SetColorAndWeaponMode(m_WhiteColor, WeaponMode.White);
                    m_ColorIndex = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (m_ColorIndex == 1)
                {
                    m_ColorChangeSound.Play();
                    SetColorAndWeaponMode(m_WhiteColor, WeaponMode.White);
                    m_ColorIndex = 0;
                }
                else if (m_ColorIndex == 0)
                {
                    m_ColorChangeSound.Play();
                    SetColorAndWeaponMode(m_YellowColor, WeaponMode.Yellow);
                    m_ColorIndex = 2;
                }
            }
        }

        private void SetColorAndWeaponMode(Color color, WeaponMode mode)
        {
            m_Renderer.color = color;
            m_Slime.SetWeaponMode(mode);
        }
    }
}

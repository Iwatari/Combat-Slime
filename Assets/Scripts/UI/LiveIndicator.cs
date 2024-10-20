using UnityEngine.UI;
using UnityEngine;

namespace CombatSlime
{

    public class LivesIndicator : MonoBehaviour
    {
        [SerializeField] private Text m_Text;

        private float lastLives;
        private void Update()
        {
            int lives = Player.Instance.NumLives;

            if (lastLives != lives)
            {
                m_Text.text = lives.ToString();
                lastLives = lives;
            }
        }
    }
}

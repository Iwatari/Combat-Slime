using UnityEngine;
using UnityEngine.UI;

namespace CombatSlime
{
    public class ResultPanel : MonoBehaviour
    {
        private const string PassedText = "Passed";
        private const string LoseText = "Lose";
        private const string RestartText = "Restart";
        private const string KillsText = "Kills: ";
        private const string TimeText = "Time: ";

        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Time;
        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonNextText;

        private bool m_LevelPassed = false;

        private void Start()
        {
            gameObject.SetActive(false);
            LevelController.Instance.LevelPassed += OnLevelPassed;
        }

        private void OnDestroy()
        {
            LevelController.Instance.LevelPassed -= OnLevelPassed;
        }
        private void OnLevelPassed()
        {
            gameObject.SetActive(true);

            m_LevelPassed = true;

            FillLevelStatistics();

            m_Result.text = PassedText;
        }
        private void OnLevelLost()
        {
            gameObject.SetActive(true);

            FillLevelStatistics();

            m_Result.text = LoseText;
            m_ButtonNextText.text = RestartText;
        }

        private void FillLevelStatistics()
        {
            m_Kills.text = KillsText + Player.Instance.NumKills.ToString();
            m_Time.text = TimeText + LevelController.Instance.LevelTime.ToString("F0");
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            if (m_LevelPassed == true)
            {
                LevelController.Instance.LoadNextLevel();
            }
            else
            {
                LevelController.Instance.RestartLevel();
            }
        }

    }
}

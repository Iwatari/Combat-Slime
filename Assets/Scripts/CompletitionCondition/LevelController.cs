using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CombatSlime
{

    public class LevelController : SingletonBase<LevelController>
    {
        private const string MainMenuSceneName = "main_menu";

        public event UnityAction LevelPassed;
        public event UnityAction LevelLost;

        [SerializeField] private LevelInfo m_LevelProperties;
        [SerializeField] private LevelCondition[] m_Conditions;
        [SerializeField] private AudioSource m_WictorySound;

        private bool m_IsLevelCompleted;
        private float m_LevelTime;

        public bool HasNextLevel => m_LevelProperties.NextLevel != null;
        public float LevelTime => m_LevelTime;

        private void Start()
        {
            Time.timeScale = 1;
            m_LevelTime = 0;
        }
        private void Update()
        {
            if(m_IsLevelCompleted == false)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelConditions();
            }

            if (Player.Instance.NumLives == 0)
            {
                Lose();
            }
        }

        private void CheckLevelConditions()
        {

            int numCompleted = 0;

            for (int i = 0; i < m_Conditions.Length; i++)
            {
                if (m_Conditions[i].IsCompleted == true)
                {
                    numCompleted++;
                }
            }
            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;
                Pass();
            }
        }
        private void Lose()
        {
            LevelLost?.Invoke();
            Time.timeScale = 0;
        }

        private void Pass()
        {
            m_WictorySound.Play();
            LevelPassed?.Invoke();
            Time.timeScale = 0;
        }

        public void LoadNextLevel()
        {
            if (HasNextLevel == true)
                SceneManager.LoadScene(m_LevelProperties.NextLevel.SceneName);
            else
                SceneManager.LoadScene(MainMenuSceneName);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(m_LevelProperties.SceneName);
        }
    }
}

using UnityEngine;

namespace CombatSlime
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives;
        [SerializeField] private Slime m_PlayerSlimePrefab;
         private CameraController m_CameraController;

        private Slime m_Slime;
        private int m_Score;
        private int m_NumKills;

        public int Score => m_Score;
        public int NumKills => m_NumKills;
        public int NumLives => m_NumLives;
        public Slime ActiveSlime => m_Slime;
        public void Construct(CameraController cameraController, Slime slime)
        {
            m_CameraController = cameraController;
            m_Slime = slime;
        }

        private void Start()
        {
            m_Slime.EventOnDeath.AddListener(OnSlimeDeath);
        }

        private void OnSlimeDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
                Invoke("Respawn", 1.5f);
        }

        private void Respawn()
        {
            var newPlayerSlime = Instantiate(m_PlayerSlimePrefab);

            m_Slime = newPlayerSlime.GetComponent<Slime>();

            m_Slime.EventOnDeath.AddListener(OnSlimeDeath);

            m_CameraController.SetTarget(m_Slime.transform);
            m_CameraController.SetTarget(m_Slime.transform);

        }

        public void AddKill()
        {
            m_NumKills += 1;
        }

        public void AddScore(int num)
        {
            m_Score += num;
        }
    }
}
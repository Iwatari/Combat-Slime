using UnityEngine;

namespace CombatSlime
{
    public class LevelBuilder : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject playerHUDPrefab;
        [SerializeField] private GameObject levelGUIPrefab;

        [Header("Dependencies")]
        [SerializeField] private PlayerSpawner m_PlayerSpawner;
        [SerializeField] private LevelController levelController;

        private void Awake()
        {
            levelController.Init();
            Player player = m_PlayerSpawner.Spawn();
            player.Init();

            Instantiate(playerHUDPrefab);
            Instantiate(levelGUIPrefab);
        }

    }
}

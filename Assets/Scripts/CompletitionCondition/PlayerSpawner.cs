using UnityEngine;

namespace CombatSlime
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private CameraController m_CameraControllerPrefab;
        [SerializeField] private Player m_PlayerPrefab;
        [SerializeField] private Slime m_SlimePrefab;

        public Player Spawn()
        {
            CameraController cameraController = Instantiate(m_CameraControllerPrefab);

            Slime slime = Instantiate(m_SlimePrefab);

            Player player = Instantiate(m_PlayerPrefab);
            player.Construct(cameraController, slime);

            cameraController.SetTarget(slime.transform);
            return player;
        }
    }
}

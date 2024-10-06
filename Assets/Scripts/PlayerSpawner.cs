using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSlime
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private CameraController m_CameraControllerPrefab;
        [SerializeField] private Player m_PlayerPrefab;
        [SerializeField] private Slime m_MovementControllerPrefab;

        [SerializeField] private Transform m_SpawnPoint;

        public Player Spawn()
        {
            CameraController cameraController = Instantiate(m_CameraControllerPrefab);

            Slime movementController = Instantiate(m_MovementControllerPrefab);

            Player player = Instantiate(m_PlayerPrefab);
            player.Construct(cameraController, movementController, m_SpawnPoint);
            return player;
        }
    }
}

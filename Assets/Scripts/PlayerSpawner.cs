using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSlime
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private Player m_PlayerPrefab;
        [SerializeField] private Slime m_SlimePrefab;

        [SerializeField] private Transform m_SpawnPoint;

        public Player Spawn()
        {
            CameraController cameraController = Instantiate(m_CameraController);

            Slime slime = Instantiate(m_SlimePrefab);

            Player player = Instantiate(m_PlayerPrefab);
            player.Construct(cameraController, slime, m_SpawnPoint);

            cameraController.SetTarget(slime.transform);
            return player;
        }
    }
}

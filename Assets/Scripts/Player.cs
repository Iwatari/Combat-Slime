using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

namespace CombatSlime
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives;
        [SerializeField] private Slime m_Slime;
        [SerializeField] private GameObject m_PlayerSlimePrefab;
        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private Slime m_MovementController;

        private Transform m_SpawnPoint;
        public void Construct(CameraController cameraController, Slime movementController, Transform spawnPoint)
        {
            m_CameraController = cameraController;
            m_MovementController = movementController;
            m_SpawnPoint = spawnPoint;
        }
        private void Start()
        {
            m_Slime.EventOnDeath.AddListener(OnSlimeDeath);
        }

        private void OnSlimeDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
                Respawn();
        }

        private void Respawn()
        {
            var newPlayerSlime = Instantiate(m_PlayerSlimePrefab);

            m_Slime = newPlayerSlime.GetComponent<Slime>();

            m_CameraController.SetTarget(m_Slime.transform);
        }
    }
}
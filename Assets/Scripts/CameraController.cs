using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSlime
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private Transform player;
        [SerializeField] private float m_SmoothSpeed = 0.125f;
        [SerializeField] private float m_HorizontalOffset = 5f;

        private Vector3 m_TargetPosition;
        private float m_RightBound;
        private float m_LeftBound;
        private float m_FixedZPosition;

        private void Start()
        {
            UpdateBounds();
            m_FixedZPosition = m_Camera.transform.position.z;
        }
        private void LateUpdate()
        {
            if (player != null)
            {
                CameraFollowPlayer();
            }
        }

        private void UpdateBounds()
        {
            m_RightBound = m_Camera.transform.position.x + m_HorizontalOffset;
            m_LeftBound = m_Camera.transform.position.x - m_HorizontalOffset;
        }

        private void CameraFollowPlayer()
        {
            if (player.position.x > m_RightBound)
            {
                m_TargetPosition.x = player.position.x - m_HorizontalOffset;
            }

            else if (player.position.x < m_LeftBound)
            {
                m_TargetPosition.x = player.position.x + m_HorizontalOffset;
            }

            m_TargetPosition.z = m_FixedZPosition;
            m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, m_TargetPosition, m_SmoothSpeed);

            UpdateBounds();
        }


        private void OnDrawGizmos()
        {
            if (player != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector3(m_RightBound, -10, 0), new Vector3(m_RightBound, 10, 0));
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(new Vector3(m_LeftBound, -10, 0), new Vector3(m_LeftBound, 10, 0));
            }
        }
    }
}

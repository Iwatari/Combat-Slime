using UnityEngine;

namespace CombatSlime
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private Transform m_Target;
        [SerializeField] private float m_SmoothSpeed = 0.125f;
        [SerializeField] private float m_HorizontalOffset = 5f;
        [SerializeField] private float m_VerticalOffset = 5f;

        private Vector3 m_TargetPosition;
        private float m_RightBound;
        private float m_LeftBound;
        private float m_TopBound;
        private float m_BottomBound;
        private float m_FixedZPosition;

        private void Start()
        {
            UpdateBounds();
            m_FixedZPosition = m_Camera.transform.position.z;
        }
        private void LateUpdate()
        {
            if (m_Target != null)
            {
                CameraFollowPlayer();
            }
        }

        private void UpdateBounds()
        {
            m_RightBound = m_Camera.transform.position.x + m_HorizontalOffset;
            m_LeftBound = m_Camera.transform.position.x - m_HorizontalOffset;

            m_TopBound = m_Camera.transform.position.y + m_VerticalOffset;
            m_BottomBound = m_Camera.transform.position.y - m_VerticalOffset;
        }

        private void CameraFollowPlayer()
        {
            if (m_Target.position.x > m_RightBound)
            {
                m_TargetPosition.x = m_Target.position.x - m_HorizontalOffset;
            }

            else if (m_Target.position.x < m_LeftBound)
            {
                m_TargetPosition.x = m_Target.position.x + m_HorizontalOffset;
            }
            else
            {
                m_TargetPosition.x = m_Camera.transform.position.x;
            }

            if (m_Target.position.y > m_TopBound)
            {
                m_TargetPosition.y = m_Target.position.y - m_VerticalOffset;
            }

            else if (m_Target.position.y < m_BottomBound)
            {
                m_TargetPosition.y = m_Target.position.y + m_VerticalOffset;
            }
            else
            {
                m_TargetPosition.y = m_Camera.transform.position.y; 
            }

            m_TargetPosition.z = m_FixedZPosition;
            m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, m_TargetPosition, m_SmoothSpeed);

            UpdateBounds();
        }


        private void OnDrawGizmos()
        {
            if (m_Target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector3(m_RightBound, -10, 0), new Vector3(m_RightBound, 10, 0));
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(new Vector3(m_LeftBound, -10, 0), new Vector3(m_LeftBound, 10, 0));
                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector3(-5, m_TopBound, 0), new Vector3(5, m_TopBound, 0));
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(new Vector3(-5, m_BottomBound, 0), new Vector3(5, m_BottomBound, 0));
            }
        }

        public void SetTarget(Transform newTarget)
        {
            m_Target = newTarget;
        }
    }
}

using UnityEngine;
using Common;

namespace CombatSlime
{
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol,
            PatrolRoute,
            Attack
        }

        [Header("Настройки поведения AI")]
        [SerializeField] private AIBehaviour m_AIBehaviour = AIBehaviour.Patrol;

        [Header("Настройки патрулирования")]
        [SerializeField] private Transform[] m_PatrolPoints;
        [SerializeField] private float m_PatrolSpeed = 0.5f;
        [SerializeField] private float m_WaitTimeAtPoints = 1f;

        [Header("Настройки атаки")]
        [SerializeField] private float m_AttackRange = 5f;
        [SerializeField] private float m_AttackRate = 1f;
        [SerializeField] private WeaponMode m_AttackMode = WeaponMode.White;
        [SerializeField] private float m_TargetUpdateInterval = 1f;
        private float m_AttackCooldown = 0f;
        private float m_TargetUpdateTimer = 0f;
        private float m_RadiusSphere = 0.4f;
        private Slime m_Slime;
        private int m_CurrentPointIndex = 0;
        private bool isWaiting = false;
        private float m_WaitTimer = 0f;

        private Destructible m_SelectedTarget;

        private void Start()
        {
            m_Slime = GetComponent<Slime>();

            m_SelectedTarget = FindNearestDestructibleTarget();
        }

        private void Update()
        {
            UpdateTimers();
            UpdateTarget();
            UpdateBehaviour();
        }

        private void UpdateTimers()
        {
            if(m_AttackCooldown > 0)
                m_AttackCooldown -= Time.deltaTime;

            m_TargetUpdateTimer -= Time.deltaTime;
        }
        
        private void UpdateTarget()
        {
            if (m_TargetUpdateTimer <= 0)
            {
                m_TargetUpdateTimer = m_TargetUpdateInterval;
                m_SelectedTarget = FindNearestDestructibleTarget();
            }
        }

        private void UpdateBehaviour()
        {
            if (m_SelectedTarget != null && Vector2.Distance(transform.position, m_SelectedTarget.transform.position) <= m_AttackRange && IsDestructibleVisible(m_SelectedTarget))
            {
                m_AIBehaviour = AIBehaviour.Attack;
            }
            else
            {
                m_AIBehaviour = AIBehaviour.Patrol;
            }

            switch (m_AIBehaviour)
            {
                case AIBehaviour.Patrol:
                    Patrol();
                    break;
                case AIBehaviour.Attack:
                    Attack();
                    break;
                    //дополнительный функционал для Null
                case AIBehaviour.Null:
                default:
                    break;
            }
        }
        
        private void Patrol()
        {
            if (m_PatrolPoints.Length == 0 || m_PatrolPoints == null)
                return;

            Transform targetPoint = m_PatrolPoints[m_CurrentPointIndex];
            Vector3 direction = (targetPoint.position - transform.position).normalized;
            
            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                WaitingControl();
            }
            else
            {
                m_Slime.Move(direction.x * m_PatrolSpeed); 
            }
        }

        private void Attack()
        {
            if (m_SelectedTarget == null)
                return;

            if (!IsDestructibleVisible(m_SelectedTarget))
                return;

            Vector2 direction = (m_SelectedTarget.transform.position - transform.position).normalized;

            m_Slime.Move(direction.x * m_PatrolSpeed);

            Debug.DrawLine(transform.position, m_SelectedTarget.transform.position, Color.red);


            if (m_AttackCooldown <= 0)
            {
                m_Slime.AIFire(m_SelectedTarget.transform.position, m_AttackMode);
                m_AttackCooldown = m_AttackRate;
            }
        }

        private bool IsDestructibleVisible(Destructible target)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, target.transform.position);

            LayerMask mask = ~(LayerMask.GetMask("AI", "Projectile"));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, mask);
            if (hit.collider != null)
            {
                return hit.collider.GetComponent<Destructible>() == target;
            }
            return true;
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float minDist = float.MaxValue;
            Destructible nearestTarget = null;

            foreach (var destructible in Destructible.AllDestructible)
            {
                if (destructible == null) continue;

                if (destructible.GetComponent<AIController>() == this) continue;

                // функционал для добавлкения команд
                // if (destructible.TeamID == m_Slime.TeamID) continue; // Если у вас есть команды

                float dist = Vector2.Distance(transform.position, destructible.transform.position);
                if (dist < minDist && dist <= m_AttackRange)
                {
                    minDist = dist;
                    nearestTarget = destructible;
                }
            }

            return nearestTarget;
        }

        private void WaitingControl()
        {
            if (!isWaiting)
            {
                isWaiting = true;
                m_WaitTimer = m_WaitTimeAtPoints;
            }

            if (isWaiting)
            {
                m_Slime.Move(0);
                m_WaitTimer -= Time.deltaTime;
                if (m_WaitTimer <= 0)
                {
                    isWaiting = false;
                    m_CurrentPointIndex = (m_CurrentPointIndex + 1) % m_PatrolPoints.Length;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (m_PatrolPoints != null && m_PatrolPoints.Length > 0)
            {
                for (int i = 0; i < m_PatrolPoints.Length; i++)
                {
                    if (m_PatrolPoints[i] == null) continue;

                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(m_PatrolPoints[i].position, m_RadiusSphere);

                    if (i < m_PatrolPoints.Length - 1 && m_PatrolPoints[i + 1] != null)
                    {
                        Gizmos.DrawLine(m_PatrolPoints[i].position, m_PatrolPoints[i + 1].position);
                    }
                }
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_AttackRange);
        }

    }
}

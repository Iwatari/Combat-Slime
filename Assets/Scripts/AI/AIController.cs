using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float patrolSpeed = 2f;
        [SerializeField] private float waitTimeAtPoints = 1f;

        [Header("Настройки атаки")]
        [SerializeField] private float attackRange = 10f;
        [SerializeField] private float attackRate = 1f;
        [SerializeField] private WeaponMode attackMode = WeaponMode.Blue;
        private float attackCooldown = 0f;

        private Slime m_Slime;
        private int currentPointIndex = 0;
        private bool isWaiting = false;
        private float waitTimer = 0f;

        private Transform playerTransform;

        private void Start()
        {
            m_Slime = GetComponent<Slime>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("AIController: Игрок не найден. Убедитесь, что у игрока установлен тег 'Player'.");
            }
        }

        private void Update()
        {
            if (attackCooldown > 0)
                attackCooldown -= Time.deltaTime;

            if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
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
                case AIBehaviour.Null:
                default:
                    break;
            }
        }

        private void Patrol()
        {
            if (patrolPoints.Length == 0)
                return;

            Transform targetPoint = patrolPoints[currentPointIndex];
            Vector3 direction = (targetPoint.position - transform.position).normalized;

            m_Slime.Move(direction.x * patrolSpeed);

            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                if (!isWaiting)
                {
                    isWaiting = true;
                    waitTimer = waitTimeAtPoints;
                }
            }

            if (isWaiting)
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f)
                {
                    isWaiting = false;
                    currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
                }
            }
        }

        private void Attack()
        {
            if (playerTransform == null)
                return;

            if (!IsPlayerVisible())
                return;

            Vector2 direction = (playerTransform.position - transform.position).normalized;

            m_Slime.Move(direction.x * patrolSpeed);

            if (attackCooldown <= 0f)
            {
                m_Slime.AIFire(playerTransform.position, attackMode);
                attackCooldown = attackRate;
            }
        }

        private bool IsPlayerVisible()
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, playerTransform.position);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, ~LayerMask.GetMask("AI", "Projectile"));
            if (hit.collider != null)
            {
                return hit.collider.gameObject.layer == LayerMask.NameToLayer("Player");
            }
            return true;
        }
        private void OnDrawGizmos()
        {
            if (patrolPoints != null && patrolPoints.Length > 0)
            {
                for (int i = 0; i < patrolPoints.Length; i++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(patrolPoints[i].position, 0.2f);

                    if (i < patrolPoints.Length - 1)
                    {
                        Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                    }
                }
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace CombatSlime
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Slime : Destructible
    {
        [SerializeField] private float m_Mass;
        [SerializeField] private float m_MovementSpeed;
        [SerializeField] private float m_JumpForce;
        [SerializeField] private Vector2 m_GroundCheckSize;
        [SerializeField] private float m_GroundCheckAngle;
        [SerializeField] private Transform m_GroundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Weapon[] m_Weapons;

        private bool isGround;
        private Rigidbody2D m_Rigid;

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
        }

        private void Update()
        {
            UpdateRigidBody();
            CheckGround();
            JumpControl();
            MouseFireControl();
        }

        private void UpdateRigidBody()
        {
            if (!IsControlledByAI())
            {
                float move = Input.GetAxis("Horizontal");
                Move(move);
            }
        }
        public void Move(float direction)
        {
            m_Rigid.velocity = new Vector2(direction * m_MovementSpeed, m_Rigid.velocity.y);
        }

        private void CheckGround()
        {
            Collider2D hit = Physics2D.OverlapBox(m_GroundCheck.position, m_GroundCheckSize, m_GroundCheckAngle, groundLayer);
            isGround = hit != null;

            if (m_Rigid.velocity.y >= m_MovementSpeed)
            {
                isGround = false;
            }
        }

        private void JumpControl()
        {
            if (!IsControlledByAI())
            {
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGround)
                {
                    m_Rigid.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
                    isGround = false;
                }
            }
        }
        private void MouseFireControl()
        {
            if (!IsControlledByAI())
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    FireAtPosition(mousePosition, WeaponMode.Blue);
                }
            }
        }

        public void FireAtPosition(Vector3 targetPosition, WeaponMode mode)
        {
            Vector2 direction = (targetPosition - transform.position).normalized;

            for (int i = 0; i < m_Weapons.Length; i++)
            {
                if (m_Weapons[i].Mode == mode)
                {
                    m_Weapons[i].Fire(direction);
                }
            }
        }

        public void AssignWeapon(WeaponProperties props)
        {
            for (int i = 0; i < m_Weapons.Length; i++)
            {
                m_Weapons[i].AssignLoadout(props);
            }
        }

        public void AIFire(Vector3 targetPosition, WeaponMode mode)
        {
            FireAtPosition(targetPosition, mode);
        }
        private bool IsControlledByAI()
        {
            return GetComponent<AIController>() != null;
        }

        public bool IsPlayer()
        {
            return gameObject.layer == LayerMask.NameToLayer("Player");
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (m_GroundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(m_GroundCheck.position, m_GroundCheckSize);
            }
        }
#endif
    }
}

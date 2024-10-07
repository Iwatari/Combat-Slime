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

        private bool isGround;

        private Rigidbody2D m_Rigid;
        protected override void Start()
        {
            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
        }
        private void Update()
        {
            UpdateRigidBody();
            CheckGround();
            HandleJump();
            OpenFire();
        }

        private void UpdateRigidBody()
        {
            float move = Input.GetAxis("Horizontal");

            m_Rigid.velocity = new Vector2(move * m_MovementSpeed, m_Rigid.velocity.y);
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

        private void HandleJump()
        {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGround)
            {
                m_Rigid.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
                isGround = false;
            }
        }

        [SerializeField] private Weapon[] m_Weapons;

        public void Fire(WeaponMode mode)
        {
            for (int i = 0; i < m_Weapons.Length; i++)
            {
                if (m_Weapons[i].Mode == mode)
                {
                    m_Weapons[i].Fire();
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

        public void OpenFire()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Fire(WeaponMode.Blue);
            }
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
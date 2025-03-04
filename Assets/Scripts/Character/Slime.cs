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
        [SerializeField] private AudioSource m_JumpSound;

        private float m_AddedSpeed;
        private bool isGround;
        private Rigidbody2D m_Rigid;
        private Animator m_Animator;
        private SpriteRenderer m_SpriteRenderer;
        private WeaponMode m_Mode;
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Animator = GetComponentInChildren<Animator>();

            m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

            if (m_Animator == null)
            {
                Debug.LogError("Slime: Animator �� ������ �� �������.");
            }
        }

            private void Update()
        {
            UpdateRigidBody();
            CheckGround();
            JumpControl();
            MouseFireControl();
            UpdateAnimatorParameters();
        }

        private void UpdateRigidBody()
        {
            if (!IsControlledByAI())
            {
                float move = Input.GetAxis("Horizontal");
                Move(move);
                FlipSprite(move);
            }
        }
        public void Move(float direction)
        {
            m_Rigid.velocity = new Vector2(direction * m_MovementSpeed, m_Rigid.velocity.y);
        }

        private void CheckGround()
        {
            if (m_GroundCheck == null)
            {
                Debug.LogWarning("Slime: GroundCheck �� ��������.");
                isGround = false;
                return;
            }

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
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGround)
                {
                    m_Rigid.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
                    isGround = false;
                    m_JumpSound.Play();
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
                    FireAtPosition(mousePosition, m_Mode);
                }
            }
        }

        public void SetWeaponMode(WeaponMode mode)
        {
            m_Mode = mode;
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

        private void FlipSprite(float move)
        {
            if (move > 0)
                m_SpriteRenderer.flipX = false;  
            else if (move < 0)
                m_SpriteRenderer.flipX = true; 
        }
        #region AI
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

        //������������
        private void UpdateAnimatorParameters()
        {
            m_Animator.SetBool("isJump", !isGround && m_Rigid.velocity.y > 0.1f);
            m_Animator.SetBool("isFalling", m_Rigid.velocity.y < -0.1f);
            m_Animator.SetBool("isMoving", Mathf.Abs(m_Rigid.velocity.x) > 0.1f);
        }
        #endregion

        #region PowerUps
        public void AddSpeed(float duration, int speed)
        {
            SetSpeed(speed);
            Invoke(nameof(DeactivateSpeed), duration);
        }
        public void SetSpeed(int speed)
        {
            m_MovementSpeed += speed;
            m_AddedSpeed = speed;
        }

        private void DeactivateSpeed()
        {
            m_MovementSpeed -= m_AddedSpeed;
            m_AddedSpeed = 0;
        }

        public void AddHealth(int healthAmount)
        {
            m_CurrentHitPoints += healthAmount;

            if (m_CurrentHitPoints > m_HitPoints)
            {
                m_CurrentHitPoints = m_HitPoints;
            }
        }
        #endregion


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

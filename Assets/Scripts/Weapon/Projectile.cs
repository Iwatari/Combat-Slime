using UnityEngine;
using Common;

namespace CombatSlime
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_Lifetime;
        [SerializeField] protected int m_Damage;
        [SerializeField] protected LayerMask m_CollisionLayer;
        [SerializeField] protected WeaponMode weaponColor;

        private float m_Timer;
        protected Destructible m_Parent;

        protected virtual void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength, m_CollisionLayer);

            if (hit)
            {
                Destructible target = hit.collider.transform.root.GetComponent<Destructible>();

                if (target != null && target != m_Parent && IsDamageValid(target))
                {
                    target.AplyDamage(m_Damage);
                    if(target.HitPoints <= 0)
                    {
                        Player.Instance.AddKill();
                    }
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                OnProjectileLifeEnd(null, transform.position);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        public void SetParentShooter(Destructible parent, LayerMask collisionLayers)
        {
            m_Parent = parent;
            m_CollisionLayer = collisionLayers;
        }

        private bool IsDamageValid(Destructible target)
        {
            var targetColorController = target.GetComponent<AIColorController>();

            if (targetColorController == null || m_Parent.GetComponent<AIColorController>() != null)
            {
                return true;
            }

            return weaponColor switch
            {
                WeaponMode.White => targetColorController.GetCurrentColor() == AIColorController.SlimeColor.White,
                WeaponMode.Blue => targetColorController.GetCurrentColor() == AIColorController.SlimeColor.Blue,
                WeaponMode.Yellow => targetColorController.GetCurrentColor() == AIColorController.SlimeColor.Yellow,
                _ => false
            };
        }

        protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }
    }
}

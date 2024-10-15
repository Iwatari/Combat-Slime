using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public abstract class ProjectileBase : Entity
    {
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_Lifetime;
        [SerializeField] protected int m_Damage;

        private float m_Timer;
        protected Destructible m_Parent;
        protected LayerMask m_CollisionLayer;

        protected virtual void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength, m_CollisionLayer);

            if (hit)
            {
                if (hit.collider != null)
                {
                    OnHit(hit.collider);

                    Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                    if (dest != null && dest != m_Parent)
                    {
                        dest.AplyDamage(m_Damage);
                        OnHit(dest);
                    }
                    OnProjectileLifeEnd(hit.collider, hit.point);
                }
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

        protected virtual void OnHit(Destructible destructible) { }
        protected virtual void OnHit(Collider2D collider2D) { }
        protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pos) { }
    }
}

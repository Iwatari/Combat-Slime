using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSlime;

namespace Common
{
    public abstract class ProjectileBase : Entity
    {
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_Lifetime;
        [SerializeField] protected int m_Damage;
        [SerializeField] protected LayerMask m_CollisionLayer;

        private float m_Timer;
        protected Destructible m_Parent;
        [SerializeField] protected WeaponMode weaponColor; // Цвет снаряда

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

                    Destructible target = hit.collider.transform.root.GetComponent<Destructible>();

                    if (target != null && target != m_Parent)
                    {
                        // Проверяем, можно ли нанести урон.
                        if (IsDamageValid(target))
                        {
                            target.AplyDamage(m_Damage);
                        }
                        OnHit(target);
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

        private bool IsDamageValid(Destructible target)
        {
            AIColorController aiControl = target.GetComponent<AIColorController>();

            // Если цель не содержит AIControl (например, игрок), урон всегда проходит.
            if (aiControl == null)
            {
                return true;
            }

            // Если атакующий — бот, урон всегда проходит.
            if (m_Parent.GetComponent<AIColorController>() != null)
            {
                return true;
            }

            // Сравнение цвета для обычных объектов.
            bool result = aiControl.GetCurrentColor() switch
            {
                AIColorController.SlimeColor.White => weaponColor == WeaponMode.White,
                AIColorController.SlimeColor.Blue => weaponColor == WeaponMode.Blue,
                AIColorController.SlimeColor.Yellow => weaponColor == WeaponMode.Yellow,
                _ => false
            };

            return result;
        }

        protected virtual void OnHit(Destructible destructible) { }
        protected virtual void OnHit(Collider2D collider2D) { }
        protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pos) { }
    }
}

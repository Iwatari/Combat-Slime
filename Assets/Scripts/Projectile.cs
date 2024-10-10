using UnityEngine;
using Common;

namespace CombatSlime
{
    public class Projectile : ProjectileBase
    {
        [SerializeField] private LayerMask collisionLayer;
        private ContactFilter2D filter;
        private void Start()
        {
            filter = new ContactFilter2D();
            filter.useTriggers = false;
            filter.SetLayerMask(collisionLayer);
            filter.useLayerMask = true;
        }

        protected override void OnHit(Destructible destructible)
        {
            if (destructible != null)
            {
                destructible.AplyDamage(m_Damage);
            }

            Destroy(gameObject); 
        }

        protected override void OnHit(Collider2D collider)
        {
            if (m_Parent != null && collider.gameObject == m_Parent.gameObject)
            {
                return; 
            }

            Destructible destructible = collider.GetComponent<Destructible>();
            if (destructible != null && destructible != m_Parent)
            {
                OnHit(destructible);
            }
        }

        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }

    }
}

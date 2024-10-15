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
        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }

    }
}

using UnityEngine;
using Common;

namespace CombatSlime
{
    public class Projectile : ProjectileBase
    {
        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }
    }
}

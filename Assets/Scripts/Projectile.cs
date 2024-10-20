using UnityEngine;
using Common;

namespace CombatSlime
{
    public class Projectile : ProjectileBase
    {
        protected override void OnHit(Destructible destructible)
        {
            if (destructible.HitPoints <= 0)
            {
                    if (destructible is Slime && destructible.HitPoints <= 0)
                    {
                    if (Player.Instance != null)
                    {
                        Player.Instance.AddKill();
                    }
                    else
                    {
                        Debug.LogWarning("Player.Instance is null. Kill not registered.");
                    }
                }
            }
        }
    protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace CombatSlime
{
    public class Projectile : ProjectileBase
    {
        [SerializeField] public GameObject m_ImpactEffectPrefab;

        protected override void OnHit(Destructible destructible)
        {

            if (destructible.HitPoints <= 0)
            {
                if (m_Parent == Player.Instance.ActiveSlime)
                {
                    Player.Instance.AddScore(destructible.ScoreValue);

                    if (destructible is Slime)
                    {
                        if (destructible.HitPoints <= 0)
                            Player.Instance.AddKill();
                    }
                }
            }
        }
        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject, 0);
        }
    }
}

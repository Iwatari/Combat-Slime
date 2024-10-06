using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace CombatSlime
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string Tag = "Spike";
        [SerializeField] private int m_Damage;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var destructible = transform.root.GetComponent<Destructible>();
            if(collision.transform.tag == Tag)
            {
                if (destructible != null)
                {
                    destructible.AplyDamage(m_Damage);
                }
            }
        }
    }
}

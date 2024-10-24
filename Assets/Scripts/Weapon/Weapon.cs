using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSlime
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponMode m_Mode;

        public WeaponMode Mode => m_Mode;

        [SerializeField] private WeaponProperties m_WeaponProperties;

        [SerializeField] private AudioSource m_Audio;

        private float m_RefireTimer;

        private Slime m_Slime;

        private void Start()
        {
            m_Slime = transform.root.GetComponent<Slime>();
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
                m_RefireTimer -= Time.deltaTime;
        }

        public void Fire(Vector2 fireDirection)
        {
            if (m_WeaponProperties == null || m_RefireTimer > 0) return;

            Projectile projectile = Instantiate(m_WeaponProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = fireDirection;

            LayerMask collisionLayer;
            if (m_Slime.IsPlayer())
            {
                collisionLayer = LayerMask.GetMask("AI", "Ground", "Obstacle"); 
            }
            else
            {
                collisionLayer = LayerMask.GetMask("Player"); 
            }

            Projectile collisionCheck = projectile.GetComponent<Projectile>();
            if (collisionCheck != null)
            {
                collisionCheck.SetParentShooter(m_Slime, collisionLayer);
            }

            if (m_Audio != null && m_WeaponProperties.LaunchSFX != null)
            {
                m_Audio.PlayOneShot(m_WeaponProperties.LaunchSFX);
            }

            m_RefireTimer = m_WeaponProperties.RateOfFire;
            m_Audio.Play();
        }

        public void AssignLoadout(WeaponProperties props)
        {
            if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;
            m_WeaponProperties = props;
        }
    }
}

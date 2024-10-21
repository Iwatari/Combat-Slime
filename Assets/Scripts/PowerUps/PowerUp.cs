using UnityEngine;

namespace CombatSlime
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class PowerUp: MonoBehaviour
    {
        [SerializeField] private int m_TargetTeamID;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Slime slime = collision.transform.root.GetComponent<Slime>();

            if (slime != null && Player.Instance.ActiveSlime && slime.TeamID == m_TargetTeamID)
            {
                OnPickedUp(slime);

                Destroy(gameObject);
            }
        }
        protected abstract void OnPickedUp(Slime slime);
    }
}

using UnityEngine;

namespace CombatSlime
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private AudioSource m_TeleportSound;

        [SerializeField] private Portal m_Target;

        [HideInInspector] public bool IsReceive; 

        private bool isInTrigger = false; 

        private void Update()
        {
            if (isInTrigger && Input.GetKeyDown(KeyCode.F))
            {
                m_TeleportSound.Play();
                Teleport();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Slime slime = other.GetComponent<Slime>();

            if (slime != null)
            {
                isInTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Slime slime = other.GetComponent<Slime>();

            if (slime != null)
            {
                isInTrigger = false;
            }
        }

        private void Teleport()
        {
            Slime slime = Player.Instance.ActiveSlime;

            if (slime != null)
            {
                m_Target.IsReceive = true;

                slime.transform.position = m_Target.transform.position;
            }
        }
    }
}

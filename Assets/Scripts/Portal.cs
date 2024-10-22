using UnityEngine;
using Common;

namespace CombatSlime
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Portal target;

        [HideInInspector] public bool IsReceive; 

        private bool isInTrigger = false; 

        private void Update()
        {
            if (isInTrigger && Input.GetKeyDown(KeyCode.F))
            {
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
                target.IsReceive = true;

                slime.transform.position = target.transform.position;
            }
        }
    }
}

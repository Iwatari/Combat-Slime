using UnityEngine;
using UnityEngine.Events;

namespace CombatSlime
{
    public class OpenGate : MonoBehaviour
    {
        public UnityEvent Enter;
        public UnityEvent Exit;
        [SerializeField] private AudioSource m_ButtonClick;

        private Animator animator;
        private bool isPressed = false;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Slime slime = collision.collider.GetComponent<Slime>();

            if (slime != null && !isPressed)
            {
                Enter.Invoke();
                m_ButtonClick.Play();
                animator.SetTrigger("Press");
                isPressed = true; 
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            Slime slime = collision.collider.GetComponent<Slime>();

            if (slime != null)
            {
                Exit.Invoke();
                isPressed = false;
            }
        }
    }
}

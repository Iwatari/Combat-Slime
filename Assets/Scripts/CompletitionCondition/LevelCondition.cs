using UnityEngine;

namespace CombatSlime
{
    public abstract class LevelCondition : MonoBehaviour
    {
        public virtual bool IsCompleted {get; }
    }
}
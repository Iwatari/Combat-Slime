using UnityEngine;

namespace CombatSlime
{
    public class LevelCompletionScore : LevelCondition
    {
        [SerializeField] private int m_NumKills;
        public override bool IsCompleted
        {
            get
            {
                if (Player.Instance.ActiveSlime == null) return false;

                if(Player.Instance.NumKills >= m_NumKills)
                {
                    return true;
                }
                return false;
            }
        }
    }
}

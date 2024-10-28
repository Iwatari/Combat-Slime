using UnityEngine;

namespace CombatSlime
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private Slime m_Slime;
        [SerializeField] private Player m_Player;


        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Player>(m_Player, monoBehaviourInScene);
            Bind<Slime>(m_Slime, monoBehaviourInScene);
        }
        private void Awake()
        {
            FindAllObjectToBind();
        }
    }
}

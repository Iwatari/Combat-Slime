using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSlime
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private Slime slime;
        [SerializeField] private Player player;


        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Player>(player, monoBehaviourInScene);
            Bind<Slime>(slime, monoBehaviourInScene);
        }
        private void Awake()
        {
            FindAllObjectToBind();
        }
    }
}

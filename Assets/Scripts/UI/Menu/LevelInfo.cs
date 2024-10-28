using UnityEngine;

namespace CombatSlime
{
    [CreateAssetMenu]
    public class LevelInfo : ScriptableObject
    {
        [SerializeField] private string sceneName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string title;
        [SerializeField] private LevelInfo m_NextLevel;

        public string SceneName => sceneName;
        public Sprite Icon => icon;
        public string Title => title;

        public LevelInfo NextLevel => m_NextLevel;
    }
}

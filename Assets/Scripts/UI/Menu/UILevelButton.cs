using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CombatSlime
{
    public class UILevelButton : UISelectableButton, IScriptableObjectProperty
    {
        [SerializeField] private LevelInfo levelInfo;

        [SerializeField] private Image icon;
        [SerializeField] private Text title;

        private void Start()
        {
            ApplyProperty(levelInfo);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (levelInfo == null) return;

            SceneManager.LoadScene(levelInfo.SceneName);
        }

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;

            if (property is LevelInfo == false) return;

            levelInfo = property as LevelInfo;

            icon.sprite = levelInfo.Icon;
            title.text = levelInfo.Title;
        }
    }
}

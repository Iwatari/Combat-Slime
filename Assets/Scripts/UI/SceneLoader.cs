using UnityEngine;
using UnityEngine.SceneManagement;

namespace CombatSlime
{
    public class SceneLoader : MonoBehaviour
    {
        private const string MainMenuSceneTitle = "MainMenu";
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenuSceneTitle);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Reset()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}

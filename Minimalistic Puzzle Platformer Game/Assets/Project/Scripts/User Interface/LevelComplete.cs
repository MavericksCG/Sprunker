using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sprunker.UserInterface {

    public class LevelComplete : MonoBehaviour {

        [Header("Assignables")]
        public int loadNextScene;
        private LoadingBar lb;

        private void Start () {
            lb = FindObjectOfType<LoadingBar>();
            loadNextScene = SceneManager.GetActiveScene().buildIndex + 1;
        }

        // Buttons
        public void Continue () {
            // Load the next level in the queue
            SceneManager.LoadScene(loadNextScene);
        
            // Set new value for when you complete a level so that the next level is now interactable in the level selection page
            if (loadNextScene > PlayerPrefs.GetInt("currentLevel")) {
                PlayerPrefs.SetInt("currentLevel", loadNextScene);
            }
        }

        public void Retry () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitToTitle () {
            SceneManager.LoadScene(1);
        }

        public void ExitToDesktop () {
            Application.Quit();
            Debug.Log("Quitting Game");
        }

    }
    
}

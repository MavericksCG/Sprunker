using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sprunker.UserInterface {

    public class LevelComplete : MonoBehaviour {

        // Buttons
        public void Continue () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Retry () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitToMenu () {
            Debug.Log("Loading Main Menu");
        }

        public void ExitToDesktop () {
            Application.Quit();
            Debug.Log("Quitting Game");
        }

    }
    
}

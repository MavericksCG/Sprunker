using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sprunker.UserInterface {

    public class GameOver : MonoBehaviour {

        public void Retry () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Title () {
            print("Loading Menu");
        }

        public void Quit () {
            Application.Quit();
        }

    }

}
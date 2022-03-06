using UnityEngine;
using UnityEngine.SceneManagement;

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
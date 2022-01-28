using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Assignables")]
    public GameObject lcUI;
    public GameObject goUI;
    public GameObject indi;
    public GameObject player;

    public static GameManager instance;


    private void Awake () {
        instance = this;
    }


    private void Update () {
        QuickRestart();
    }

    
    private void QuickRestart () {

        if (Input.GetKey(Keybinds.instance.quickRestart)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }


    public void Complete () {
        lcUI.SetActive(true);
        indi.SetActive(false);
        Time.timeScale = 1f;
        Destroy(player);
    }

    public void EndGame () {
        goUI.SetActive(true);
        indi.SetActive(false);
        Time.timeScale = 1f;
        Destroy(player);
    }
}
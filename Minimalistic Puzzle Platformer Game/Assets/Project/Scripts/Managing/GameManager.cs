using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Assignables")]
    public GameObject lcUI;
    public GameObject goUI;
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
        Destroy(player);
    }

    public void EndGame () {
        goUI.SetActive(true);
        Destroy(player);
    }
}
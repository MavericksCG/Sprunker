using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    [Header("ASSIGNABLES")]

    public GameObject lcUI;
    public GameObject goUI;
    public GameObject indi;
    public GameObject pmUI;
    
    public GameObject player;
    public GameObject env;
    
    public static GameManager instance;

    public Color bgColour;

    [Tooltip("How fast the background colour should interpolate to the custom colour")] public float colorLerpSpeed;


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
        pmUI.SetActive(false);
        Time.timeScale = 1f;
        Destroy(player);
    }

    public void EndGame () {
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, bgColour, colorLerpSpeed * Time.deltaTime);
        goUI.SetActive(true);
        indi.SetActive(false);
        pmUI.SetActive(false);
        Time.timeScale = 1f;
        Destroy(player);
    }
}
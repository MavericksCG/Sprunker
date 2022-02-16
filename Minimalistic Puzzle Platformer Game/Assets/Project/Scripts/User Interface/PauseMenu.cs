using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

public class PauseMenu : MonoBehaviour {

    [Header("References and Variables")]
    public GameObject pausedUI;
    public TextMeshProUGUI randomPausedTextUI;

    private static bool paused = false;

    public string[] randomPausedText;

    private SlowMotion slowMotion;


    private void Start () {
        ChooseRandomText();

        slowMotion = FindObjectOfType<SlowMotion>();
    }

    private void Update () {
        if (Input.GetKeyDown(Keybinds.instance.pauseOrResume)) {
            if (paused) Resume(); else Pause();
        }

        if (pausedUI.activeInHierarchy) {
            Time.timeScale = 0f;
        }
    }

    #region Pausing and Resuming

    public void Resume () {
        pausedUI.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
        slowMotion.enabled = true;
    }

    private void Pause () {
        pausedUI.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
        slowMotion.enabled = false;
    }

    #endregion


    #region Button Handling

    public void Restart () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToDesktop () {
        Application.Quit();
    }

    public void ExitToMainMenu () {
        Debug.Log("Loading Main Menu...");
    }

    public void OpenSettings () {
        Debug.Log("Opening Settings Menu...");
    }

    #endregion

    #region Miscellaneous 

    public void ChooseRandomText () {
        int textIndex = Random.Range(0, randomPausedText.Length);

        string t = randomPausedText[textIndex];
        randomPausedTextUI.text = t;
        
    }

    #endregion
}
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

public class PauseMenu : MonoBehaviour {

    [Header("References and Variables")]
    public GameObject pausedUI;
    public TextMeshProUGUI randomPausedTextUI;

    [Range(0f, 1f)] [SerializeField] private float timeScaleLerpSpeed;

    private static bool paused = false;

    public string[] randomPausedText;


    private void Start () {
        ChooseRandomText();
    }

    private void Update () {
        if (Input.GetKeyDown(Keybinds.instance.pauseOrResume)) {
            if (paused) Resume(); else Pause();
        }
    }

    #region Pausing and Resuming

    private void Resume () {
        pausedUI.SetActive(false);
        paused = false;
        Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, timeScaleLerpSpeed);
    }

    private void Pause () {
        pausedUI.SetActive(true);
        paused = true;
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, timeScaleLerpSpeed);
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
        print("Loading Main Menu...");
    }

    public void OpenSettings () {
        print("Opening Settings Menu...");
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
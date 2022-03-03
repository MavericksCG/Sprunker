using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [Header("References and Variables")]
    public GameObject pausedUI;
    [SerializeField] private Animator pma;

    private static bool paused = false;

    private SlowMotion slowMotion;


    private void Start () {
        slowMotion = FindObjectOfType<SlowMotion>();
    }

    private void Update () {
        if (Input.GetKeyDown(Keybinds.instance.pauseOrResume)) {
            if (paused) Resume(); else Pause();
        }
        
        // Null check 
        if (pausedUI != null) {
            if (pausedUI.activeInHierarchy) {
                Time.timeScale = 0f;
            }
        }

    }

    #region Pausing and Resuming

    public void Resume () {
        pausedUI.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
        pma.SetTrigger("close");
    }

    private void Pause () {
        pausedUI.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
        pma.SetTrigger("pause");
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
}
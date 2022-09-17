using UnityEngine;
using UnityEngine.SceneManagement;
using Sprunker.Managing;
using Sprunker.Universal;

namespace Sprunker.UserInterface {

    public class PauseMenu : MonoBehaviour {

        [Header("References and Variables")] public GameObject pausedUI;
        [SerializeField] private Animator pma;

        private static bool paused = false;

        private SlowMotion slowMotion;

        [SerializeField] private GameObject settingsUI;


        private void Start () {
            slowMotion = FindObjectOfType<SlowMotion>();
        }

        private void Update () {
            if (Input.GetKeyDown(Keybinds.instance.pauseOrResume)) {
                if (paused) Resume();
                else Pause();
            }

            // Null check 
            if (pausedUI != null) {
                if (pausedUI.activeInHierarchy) {
                    Time.timeScale = 0f;
                }
            }

            if (pausedUI != null) {
                if (!pausedUI.activeInHierarchy) {
                    settingsUI.SetActive(false);
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

        public void OpenSettings () {
            settingsUI.SetActive(true);
        }

        #endregion
    }

}
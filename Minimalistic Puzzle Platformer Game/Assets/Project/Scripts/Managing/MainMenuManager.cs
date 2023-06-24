using UnityEngine;

namespace Sprunker.Managing {

    public class MainMenuManager : MonoBehaviour {

        [Header("Variables and References")]
        [SerializeField] private GameObject[] menuButtons;
        [SerializeField] private GameObject[] menus;

        [SerializeField] private float timeScaleWhenMenuIsActive = 0.3f;
        [SerializeField] [Range(0f, 1f)] private float lerpSpeed;

        /// <summary>
        /// Enabling all menu buttons when the game starts
        /// </summary>
        private void Start () {
            foreach (GameObject obj in menuButtons) {
                obj.SetActive(true);
            }
        }

        private void Update () {
            // Disable all menus if the player presses esc
            if (Input.GetKeyDown(Keybinds.instance.closeAllTabs)) {
                foreach (GameObject obj in menus) {
                    obj.SetActive(false);
                }
            }

            if (menus[0].activeInHierarchy || menus[1].activeInHierarchy || menus[2].activeInHierarchy || menus[3].activeInHierarchy) {
                Time.timeScale = Mathf.Lerp(Time.timeScale, timeScaleWhenMenuIsActive, lerpSpeed);
            }
            else {
                Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, lerpSpeed); ;
            }
        }
    }
}

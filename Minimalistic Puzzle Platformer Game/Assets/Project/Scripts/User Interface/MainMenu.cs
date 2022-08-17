using UnityEngine;
using Random = UnityEngine.Random;
using System;

namespace Sprunker.UserInterface {
    public class MainMenu : MonoBehaviour {

        [SerializeField] private GameObject[] menuContainers;

        [Header("Variables and References")]
        // Play Menu
        [SerializeField] private GameObject playButtonsContainer;
        private bool showPlayButtons;

        // Settings
        [SerializeField] private GameObject settingsOptionsContainer;
        private bool showSettingsMenu;

        // About Menu 
        [SerializeField] private GameObject aboutMenuContainer;
        private bool showAboutMenu;

        // Quit Menu
        [SerializeField] private GameObject modalWindow;

        // Sounds
        [SerializeField] private GameObject[] gameMusics;
        [SerializeField] private AudioSource[] gameMusicsAudios;
        private int randIndex;

        // Visualization
        [SerializeField] private AnimationCurve volumeCurve = new AnimationCurve();

        // Sound Interpolation
        [SerializeField] [Range(0f, 1f)] private float audioLerpSpeed;
        [SerializeField] private float volumeWhenModalIsActive;


        private void Start () {
            // Set randIndex (int) to a random number when the game starts. 
            randIndex = Random.Range(0, 2);

            Debug.Log(randIndex);

            // Depending on the number, set the corresponding GameObject to an active state.
            switch (randIndex) {
                // randIndex = 0 = First Object in the array being set to an active state
                case 0:
                    gameMusics[0].SetActive(true);
                    break;

                // randIndex = 1 = Second Object in the array being set to an active state
                case 1:
                    gameMusics[1].SetActive(true);
                    break;
            }
        }

        private void Update () {

            if (modalWindow.activeInHierarchy == true) {
                gameMusicsAudios[randIndex].volume = Mathf.Lerp(gameMusicsAudios[randIndex].volume, volumeWhenModalIsActive, audioLerpSpeed);
            }
            else {
                gameMusicsAudios[randIndex].volume = Mathf.Lerp(gameMusicsAudios[randIndex].volume, 0.5f, audioLerpSpeed);
            }

            foreach (AudioSource s in gameMusicsAudios) {
                volumeCurve.AddKey(Time.realtimeSinceStartup, s.volume);
            }
        }

        public void EnableModal () {
            modalWindow.SetActive(true);
            Debug.Log("Modal Enabled");
        }

        public void QuitGame () {
            Application.Quit();
            Debug.Log("Quitting Game...");
        }

        public void DisableModal () {
            modalWindow.SetActive(false);
            Debug.Log("Modal Disabled");
        }

        public void OpenReddit() {
            Application.OpenURL("https://www.reddit.com/user/DankMavericks/");
            Debug.Log("Clicked!");
        }

        public void OpenYouTube() {
            Application.OpenURL("https://www.youtube.com/channel/UC-GC41tCMv0TkDx0zddTK7w");
            Debug.Log("Clicked!");
        }

        public void OpenGitHub() {
            Application.OpenURL("https://github.com/MavericksCG");
            Debug.Log("Clicked!");
        }

        public void OpenItch() {
            Application.OpenURL("https://maverickscg.itch.io/");
            Debug.Log("Clicked!");
        }

    }
}
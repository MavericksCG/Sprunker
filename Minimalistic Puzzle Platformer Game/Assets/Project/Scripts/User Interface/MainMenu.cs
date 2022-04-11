using UnityEngine;
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


        private void Update () { 
            // Imposter syndrome code incoming!
            if (menuContainers[0].activeInHierarchy == true) {
                menuContainers[1].SetActive(false);
                menuContainers[2].SetActive(false);
            }
            else if (menuContainers[1].activeInHierarchy == true) {
                menuContainers[0].SetActive(false);
                menuContainers[2].SetActive(false);
            }
            else if (menuContainers[2].activeInHierarchy == true) {
                menuContainers[0].SetActive(false);
                menuContainers[1].SetActive(false);
            }
            else {
                return;
            }
        }

        public void OpenButtonMenu () {
            showPlayButtons = !showPlayButtons;
            
            if (showPlayButtons) {
                playButtonsContainer.SetActive(true);
            }
            else {
                playButtonsContainer.SetActive(false);
            }
        }

        public void OpenSettings () {
            showSettingsMenu = !showSettingsMenu;

            if (showSettingsMenu) {
                settingsOptionsContainer.SetActive(true);
            }
            else {
                settingsOptionsContainer.SetActive(false);
            }
        }

        public void OpenAboutMenu () {
            showAboutMenu = !showAboutMenu;

            if (showAboutMenu) {
                aboutMenuContainer.SetActive(true);
            }
            else {
                aboutMenuContainer.SetActive(false);
            }
        }

    }
}
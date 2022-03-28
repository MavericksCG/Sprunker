using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Sprunker.Managing;
using System;

namespace Sprunker.Debugging.Advanced {

    public class AdvancedDebugMenuHandler : MonoBehaviour {

        public static AdvancedDebugMenuHandler instance;

        [SerializeField] private Transform player;

        [SerializeField] private GameObject standardMenu;
        [SerializeField] private GameObject advancedMenuUI;
        private Keybinds binds;

        [SerializeField] private TMP_InputField sceneIndexField;

        [SerializeField] private TMP_InputField posX;
        [SerializeField] private TMP_InputField posY;
        [SerializeField] private TMP_InputField posZ;

        private bool showAdvancedMenu;
        private bool showStandardMenu;


        private void Awake () {
            // Get the keybinds script
            binds = FindObjectOfType<Keybinds>();

            // Set instance
            instance = this;
        }

        private void Update () {
            if (Input.GetKeyDown(binds.openCloseAdvancedMenu)) {
                showAdvancedMenu = !showAdvancedMenu;
            }

            if (Input.GetKeyDown(binds.openCloseStandardMenu)) {
                showStandardMenu = !showStandardMenu;
            }

            if (showStandardMenu) {
                standardMenu.SetActive(true);
            }
            else {
                standardMenu.SetActive(false);
            }

            if (showAdvancedMenu) {
                advancedMenuUI.SetActive(true);
            }
            else {
                advancedMenuUI.SetActive(false);
            }
        }

        public void LoadSpecificScene (int sceneIndex) {
            sceneIndex = Convert.ToInt32(sceneIndexField.text);
            SceneManager.LoadScene(sceneIndex);
        }

        public void SetPlayerPosition () {

            Vector3 position = new Vector3();

            // Calculate Values
            int calcX = Convert.ToInt32(posX.text);
            position.x = (float)calcX;

            int calcY = Convert.ToInt32(posY.text);
            position.y = (float)calcY;

            int calcZ = Convert.ToInt32(posZ.text);
            position.z = (float)calcZ;

            // Set Player Position
            player.position = position;
        }
    }

}

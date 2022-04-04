using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Sprunker.Player;
using Sprunker.Managing;
using System;

namespace Sprunker.Debugging.Advanced {

    public class AdvancedDebugMenuHandler : MonoBehaviour {

        // Universal
        public static AdvancedDebugMenuHandler instance;
        private PlayerController c;

        [Header("Player Speed")]
        [SerializeField] private TMP_InputField speedNorm;
        [SerializeField] private TMP_InputField speedSpri;


        [Header("Use Pulldown Ability")]
        [SerializeField] private TMP_InputField pulldownField;


        [Header("Player Jump Force")]
        [SerializeField] private TMP_InputField setForceField; 


        [Header("Enabling and Disabling menus")]
        [SerializeField] private GameObject standardMenu;
        [SerializeField] private GameObject advancedMenuUI;
        private Keybinds binds;

        [Header("Loading Specific Scene")]
        [SerializeField] private TMP_InputField sceneIndexField;

        [Header("Player Position")]
        [SerializeField] private Transform player;
        [SerializeField] private TMP_InputField posX;
        [SerializeField] private TMP_InputField posY;
        [SerializeField] private TMP_InputField posZ;

        [Header("Bool Checks")] 
        private bool showAdvancedMenu;
        private bool showStandardMenu;


        private void Awake () {
            // Get the keybinds script
            binds = FindObjectOfType<Keybinds>();

            // Get PlayerController
            c = FindObjectOfType<PlayerController>();

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

            if (player != null) {

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

        public void SetPlayerSpeed () {

            if (c != null) {

                // Conversion
                int normalSpeed = Convert.ToInt32(speedNorm.text);
                int sprintSpeed = Convert.ToInt32(speedSpri.text);

                // Setting Speed Values
                c.normalSpeed = (float)normalSpeed;
                c.sprintSpeed = (float)sprintSpeed;


            }
        }

        public void SetPlayerJumpForce () {
            if (c != null) {
                // Conversion
                int force = Convert.ToInt32(setForceField.text);

                // Set Value 
                c.jumpForce = (float)force;
            }
        }

        public void UsePulldown (bool usePulldownAbility) {
            if (usePulldownAbility) {
                c.usePulldown = true;
            }
            else {
                c.usePulldown = false;
            }
        }

        public void SetPulldownForce () {
            // Convert
            int force = Convert.ToInt32(pulldownField.text);

            // Set Value
            c.pulldownForce = (float)force;
        }
    }

}
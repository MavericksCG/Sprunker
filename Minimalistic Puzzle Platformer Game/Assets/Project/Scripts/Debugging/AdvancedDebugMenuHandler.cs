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


        [Header("Value Related")]
        [SerializeField] private TMP_InputField setForceField;
        [SerializeField] private TMP_InputField setSuperJumpForceField;


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
        private bool showPageTwo;
        

        [Header("Second Page")] [SerializeField]
        private GameObject secondMenu;
        


        private void Awake () {
            // Get the keybinds script
            binds = FindObjectOfType<Keybinds>();

            // Get PlayerController
            c = FindObjectOfType<PlayerController>();

            // Set instance
            instance = this;
        }

        private void Update () {
            if (Input.GetKeyDown(binds.openCloseAdvancedMenu)) 
                showAdvancedMenu = !showAdvancedMenu;
            

            if (Input.GetKeyDown(binds.openCloseStandardMenu)) 
                showStandardMenu = !showStandardMenu;


            if (Input.GetKeyDown(binds.openCloseSecondPage))
                showPageTwo = !showPageTwo;
                
            // Standard Menu
            if (showStandardMenu) 
                standardMenu.SetActive(true);
            
            else 
                standardMenu.SetActive(false);
            
            // Advanced Menu
            if (showAdvancedMenu) 
                advancedMenuUI.SetActive(true);
            
            else 
                advancedMenuUI.SetActive(false);
            
            // Second Page (Advanced Menu)
            if (showPageTwo)
                secondMenu.SetActive(true);
            else 
                secondMenu.SetActive(false);
            
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
                // Since the speed values are not integers and are instead float values, we cast the changed numbers as a float, even though its an integer.
                // However, this does mean that you can't be very specific about the number you enter - 1.5 is not an acceptable value, but 1 or 2 is.
                // Narrows down your options but is still quite fun to mess with.
            }
        }

        public void SetPlayerJumpForce () {
            if (c != null) {
                // Conversion
                int force = Convert.ToInt32(setForceField.text);

                // Set Value 
                c.jumpForce = (float)force;

                Debug.Log("updated");
            }
        }

        public void SetPlayerSuperJumpForce() {
            if (c != null) {
                // Conversion
                int force = Convert.ToInt32(setSuperJumpForceField.text);
                
                // Set Value
                c.superJumpForce = (float)force;
                Debug.Log("updated sj");
                // Same thing.. 
                // The Super jump value is by default not an integer, and just setting it to the force integer would return an error
                // Casting it as a float though, makes unity think that the original value that is being set is a float value and thus, is acceptable.
                // Might be other workarounds to this, something to keep in mind later I guess.

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

        public void RechargeSuperJump() {
            c.canSuperJump = true;
        }
    }

}
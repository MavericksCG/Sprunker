using UnityEngine;

namespace Sprunker.UserInterface {
    public class MainMenu : MonoBehaviour {

        private bool showPlayButtons;
        [SerializeField] private GameObject playButtonsContainer;


        public void OpenButtonMenu () {
            showPlayButtons = !showPlayButtons;
            
            if (showPlayButtons) {
                playButtonsContainer.SetActive(true);
            }
            else {
                playButtonsContainer.SetActive(false);
            }
        }

    }
}
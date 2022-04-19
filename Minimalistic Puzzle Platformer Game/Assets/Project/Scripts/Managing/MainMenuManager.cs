using UnityEngine;

namespace Sprunker.Managing {

    public class MainMenuManager : MonoBehaviour {

        [SerializeField] private GameObject[] menuButtons;
        
        /// <summary>
        /// Enabling all menu buttons when the game starts
        /// </summary>
        private void Start() {
            foreach (GameObject obj in menuButtons) {
                obj.SetActive(true);
            }
        }

    }
    
}

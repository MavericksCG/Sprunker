using UnityEngine;
using Sprunker.UserInterface;
using TMPro;

namespace Sprunker.Managing {

    public class IntroManager : MonoBehaviour {

        [Header("Assignables")]
        [SerializeField] private GameObject[] gameMusics;
        [SerializeField] private AudioSource[] gameMusicsAudios;
        [SerializeField] private GameObject quitModal;
        [SerializeField] private AudioSource type;
        [SerializeField] private TextMeshProUGUI loadingText;

        private LoadingBar bar;
        private int randIndex;


        private void Start () {
            bar = FindObjectOfType<LoadingBar>();

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
            if (Input.GetKeyDown(Keybinds.instance.introProceed)) {
                type.Play(); // Play a satisfying type sound
                // Access the LoadScene(); function in the loading bar and load the tutorial
                bar.LoadScene(1);
                loadingText.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                // Turn on modal window so as to not quit the game abruptly and give the player a chance to reconsider if they want to quit.
                type.Play();
                quitModal.SetActive(true);
            }
        }

        public void ExitToDesktopButton () {
            quitModal.SetActive(true);
        }

        public void QuitGame () {
            Application.Quit();
        }
    }
}

using UnityEngine;
using Sprunker.UserInterface;

public class IntroManager : MonoBehaviour {

    private LoadingBar bar;

    [SerializeField] private GameObject[] gameMusics;
    [SerializeField] private AudioSource[] gameMusicsAudios;
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
        if (Input.GetKeyDown(KeyCode.X)) {
            // Access the LoadScene(); function in the loading bar and load the tutorial
            bar.LoadScene(2);
        }
    }
}

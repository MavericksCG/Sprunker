using UnityEngine;
using Sprunker.UserInterface;

public class IntroManager : MonoBehaviour {

    private LoadingBar bar;


    private void Start () {
        bar = FindObjectOfType<LoadingBar>();
    }

    private void Update () {
        if (Input.GetKeyDown(KeyCode.X)) {
            // Access the LoadScene(); function in the loading bar and load the second scene in the queue.
            bar.LoadScene(1);
        }
    }

}

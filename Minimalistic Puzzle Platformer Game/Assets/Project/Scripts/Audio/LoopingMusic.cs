using UnityEngine;

public class LoopingMusic : MonoBehaviour {

    private void Awake () {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music Object");
        if (musicObjects.Length > 1) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

}

using UnityEngine;

public class LoopingMusic : MonoBehaviour {

    private static LoopingMusic instance;

    private void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else {
            Destroy(gameObject);
        }
    }
}
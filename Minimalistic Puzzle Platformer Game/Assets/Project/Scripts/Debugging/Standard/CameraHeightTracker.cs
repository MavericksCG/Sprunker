using UnityEngine;
using TMPro;

public class CameraHeightTracker : MonoBehaviour {

    private GameObject cam;
    [SerializeField] private TextMeshProUGUI cameraHeightUI;

    private void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update () {
        cameraHeightUI.text = "camera height : " + cam.transform.position.y.ToString();
    }

}

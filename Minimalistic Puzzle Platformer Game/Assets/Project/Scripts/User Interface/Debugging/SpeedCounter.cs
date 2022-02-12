using UnityEngine;
using TMPro;


public class SpeedCounter : MonoBehaviour {

    public TextMeshProUGUI speedUI;
    public GameObject player;
    public PlayerController controller;

    private void Update () {

        if (player != null) {
            speedUI.text = "Speed while moving : " + controller.moveSpeed.ToString();
        } else {
            return;
        }
    }
}

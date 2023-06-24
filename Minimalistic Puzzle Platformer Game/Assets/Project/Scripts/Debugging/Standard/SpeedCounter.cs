using UnityEngine;
using TMPro;
using Sprunker.Player;

namespace Sprunker.Debugging.Stanadard {
    public class SpeedCounter : MonoBehaviour {

        public TextMeshProUGUI speedUI;
        public GameObject player;
        public PlayerController controller;

        private void Update () {

            if (player != null) {
                speedUI.text = "player speed : " + controller.moveSpeed.ToString();
            }
            else {
                return;
            }
        }
    }
}

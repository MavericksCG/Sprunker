using UnityEngine;
using TMPro;
using Sprunker.Player;

namespace Sprunker.UserInterface {

    public class DashIndicator : MonoBehaviour {

        [Header("VARIABLES")] [Space] public TextMeshProUGUI superJumpIndicatorUI;

        public PlayerController controller;


        private void Update () {
            if (controller != null) {
                if (controller.canDash) {
                    superJumpIndicatorUI.text = "Dash : AVAILABLE";
                }
                else if (!controller.canDash) {
                    superJumpIndicatorUI.text = "Dash : UNAVAILABLE";
                }
            }
            else {
                superJumpIndicatorUI.text = "Can't Locate <color=red>DashIndicator.cs</color> in Project/Scripts/User Interface";
            }

        }
    }
}
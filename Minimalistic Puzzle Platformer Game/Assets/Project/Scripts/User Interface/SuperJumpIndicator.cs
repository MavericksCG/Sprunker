using UnityEngine;
using TMPro;
using Sprunker.Player;

namespace Sprunker.UserInterface {

	public class SuperJumpIndicator : MonoBehaviour {

		[Header("VARIABLES"), Space] public TextMeshProUGUI superJumpIndicatorUI;

		public PlayerController controller;


		private void Update () {
			if (controller != null) {
				if (controller.canSuperJump) {
					superJumpIndicatorUI.text = "Super Jump : READY";
				}
				else if (!controller.canSuperJump) {
					superJumpIndicatorUI.text = "Super Jump : RECHARGING...";
				}
			}
			else {
				superJumpIndicatorUI.text = "Can't Locate <color=red>SuperJumpIndicator.cs</color> in Assets/Project/Scripts/User Interface!";
			}

		}
	}

}

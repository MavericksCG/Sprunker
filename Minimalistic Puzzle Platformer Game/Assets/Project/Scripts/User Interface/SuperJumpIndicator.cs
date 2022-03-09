using UnityEngine;
using TMPro;
using Sprunker.Player;

namespace Sprunker.UserInterface {

	public class SuperJumpIndicator : MonoBehaviour {

		[Header("VARIABLES")] [Space] public TextMeshProUGUI superJumpIndicatorUI;

		public PlayerController controller;


		private void Update () {
			if (controller != null) {
				if (controller.canSuperJump) {
					superJumpIndicatorUI.text = "Super Jump State : <b>AVAILABLE</b>";
				}
				else if (!controller.canSuperJump) {
					superJumpIndicatorUI.text = "Super Jump State : <b>UNAVAILABLE</b>";
				}
			}
			else {
				superJumpIndicatorUI.text = "Can't Locate <b><color=red>Indicator.cs</color></b> in Project/Scripts/User Interface";
			}

		}
	}

}

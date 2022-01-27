using UnityEngine;
using TMPro;

public class SuperJumpIndicator : MonoBehaviour {
	
	[Header("Variables")] [Space] 
	public TextMeshProUGUI superJumpIndicatorUI;

	public PlayerController controller;


	private void Update () {
		if (controller != null) {
			if (controller.canSuperJump) {
				superJumpIndicatorUI.text = "Super Jump State : <b><color=#00a8ff>AVAILABLE</color></b>";
			} else if (!controller.canSuperJump) {
				superJumpIndicatorUI.text = "Super Jump State : <b><color=#273c75>UNAVAILABLE</color></b>";
			}
		}
		else {
			superJumpIndicatorUI.text = "Can't Locate <b><color=red>Indicator.cs</color></b> in Project/Scripts/User Interface";
		}
		
	}
}

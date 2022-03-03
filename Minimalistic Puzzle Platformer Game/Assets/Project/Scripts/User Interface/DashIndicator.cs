using UnityEngine;
using TMPro;

public class DashIndicator : MonoBehaviour {
	
    [Header("VARIABLES")] [Space] 
    public TextMeshProUGUI superJumpIndicatorUI;

    public PlayerController controller;


    private void Update () {
        if (controller != null) {
            if (controller.canDash) {
                superJumpIndicatorUI.text = "Dash State : <b>AVAILABLE</b>";
            } else if (!controller.canDash) {
                superJumpIndicatorUI.text = "Dash State : <b>UNAVAILABLE</b>";
            }
        }
        else {
            superJumpIndicatorUI.text = "Can't Locate <b><color=red>DashIndicator.cs</color></b> in Project/Scripts/User Interface";
        }
		
    }
}
using UnityEngine;
using TMPro;


public class DebugPulldownIndicator : MonoBehaviour {

    public TextMeshProUGUI pulldownIndicatorUI;

    private void Update () {

        if (Input.GetKey(Keybinds.instance.pulldownKey) || Input.GetKey(Keybinds.instance.altPulldownKey)) {
            pulldownIndicatorUI.text = "Is Pulldown Active = <color=green>True</color>";
        }
        else {
            pulldownIndicatorUI.text = "Is Pulldown Active = <color=red>False</color>";
        }

    }
}

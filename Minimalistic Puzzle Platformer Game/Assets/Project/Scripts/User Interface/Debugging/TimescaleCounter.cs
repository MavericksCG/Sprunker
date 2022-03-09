using UnityEngine;
using TMPro;

namespace Sprunker.UserInterface.Debugging {
    public class TimescaleCounter : MonoBehaviour {

        [Header("References and Variables")] [SerializeField]
        private TextMeshProUGUI counterUI;


        private void Update () {
            counterUI.text = "Timescale : " + Time.timeScale.ToString();
        }

    }
}

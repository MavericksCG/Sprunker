using UnityEngine;
using TMPro;

namespace Sprunker.Debugging.Standard {
    public class TimescaleCounter : MonoBehaviour {

        [Header("References and Variables")] [SerializeField]
        private TextMeshProUGUI counterUI;


        private void Update () {
            counterUI.text = "timescale : " + Time.timeScale.ToString();
        }

    }
}

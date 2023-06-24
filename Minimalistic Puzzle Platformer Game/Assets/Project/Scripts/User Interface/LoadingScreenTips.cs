using UnityEngine;
using TMPro;

namespace Sprunker.UserInterface {

    public class LoadingScreenTips : MonoBehaviour {
        
        public string[] tips;
        [SerializeField] private TextMeshProUGUI tipText;

        private void Start () {
            if (tipText != null) {
                int rand = Random.Range(0, tips.Length);
                tipText.text = tips[rand];

            }
        }
    }

}

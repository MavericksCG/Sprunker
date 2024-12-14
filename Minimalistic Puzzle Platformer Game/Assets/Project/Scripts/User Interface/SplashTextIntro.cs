using TMPro;
using UnityEngine;

namespace Sprunker.UserInterface {

    public class SplashTextIntro : MonoBehaviour {

        [Header("References")] 
        [SerializeField] private TextMeshProUGUI splashTextRef;

        private int randNum;
        
            
        public string[] splashTexts = {
            "now on itch.io",
            "follow on social media!",
            "zzz"
        };

        private void Start() {
            randNum = Random.Range(0, splashTexts.Length);
            splashTextRef.text = splashTexts[randNum];
        }    

    }

}

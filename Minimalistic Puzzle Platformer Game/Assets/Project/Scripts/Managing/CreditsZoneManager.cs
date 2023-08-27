using UnityEngine;

namespace Sprunker.Managing {

    public class CreditsZoneManager : MonoBehaviour {

        [SerializeField] private GameObject[] texts;
        private int textLoad = 0;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                textLoad++;
                texts[textLoad - 1].SetActive(false);
                texts[textLoad].SetActive(true);
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                textLoad--;
                texts[textLoad + 1].SetActive(false);
                texts[textLoad].SetActive(true);
            }
        }
    }

}

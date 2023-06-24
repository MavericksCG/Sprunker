using UnityEngine;
using TMPro;

namespace Sprunker.UserInterface {
    public class SandboxAuth : MonoBehaviour {

        [Header("Assignables/Variables")]
        private SandboxIDNames names;

        [SerializeField] private TextMeshProUGUI playerText;
        private int randNum;
        private int nameIndex;

        [SerializeField] private int minVal;
        [SerializeField] private int maxVal;

        private void Awake () {
            names = FindObjectOfType<SandboxIDNames>();
            randNum = Random.Range(minVal, maxVal);
        } 

        private void Start () {
            nameIndex = Random.Range(0, names.names.Length);
            playerText.text = "Local Session ID - " + names.names[nameIndex] + randNum.ToString();
        }

    }
}
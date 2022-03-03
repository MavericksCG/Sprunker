using UnityEngine;
using TMPro;

public class TimescaleCounter : MonoBehaviour {
    
    [Header("References and Variables")]
    [SerializeField] private TextMeshProUGUI counterUI;


    private void Update () {
        counterUI.text = "Timescale : " + Time.timeScale.ToString();
    }

}

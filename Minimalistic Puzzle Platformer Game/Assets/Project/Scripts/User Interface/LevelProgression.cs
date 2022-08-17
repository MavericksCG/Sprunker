using UnityEngine;
using UnityEngine.UI;

public class LevelProgression : MonoBehaviour {

    [Header("References and Variables")]
    public Button[] levelButtons;


    private void Start () {
        int currentLevel = PlayerPrefs.GetInt("currentLevel");

        for (int i = 0; i < levelButtons.Length; i++) {
            levelButtons[i].interactable = false;
        }

        for (int i = 0; i < currentLevel; i++) {
            levelButtons[i].interactable = true;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    [Header("ASSIGNABLES")]

    public GameObject lcUI;
    public GameObject goUI;
    public GameObject indi;
    public GameObject pmUI;
    public GameObject pmObj;
    
    public GameObject player;
    public GameObject env;

    public GameObject audio;

    public static GameManager instance;

    private AudioSource[] sources;
    
    public Color bgColour;

    [Tooltip("How fast the background colour should interpolate to the custom colour")] public float colorLerpSpeed;
    [Range(0f, 1f)] [SerializeField] private float audioLerpSpeed;
    [SerializeField] private float pitchWhenPauseMenuIsActive;

    private void Awake () {
        instance = this;
        
        // Get all components in the audio source game object's children
        sources = audio.GetComponentsInChildren<AudioSource>();
    }


    private void Update () {
        QuickRestart();
        ChangeAudioPitches();
    }

    private void ChangeAudioPitches () {
        foreach (AudioSource s in sources) {
            // If the pause menu object is not null, execute all other code
            if (pmObj != null) {
                // Check if the pause menu is active in the hierarchy
                if (pmObj.activeInHierarchy) {
                    // Set pitches to pitchWhenPauseMenuIsActive (Long Name ngl)
                    s.pitch = Mathf.Lerp(s.pitch, pitchWhenPauseMenuIsActive, audioLerpSpeed);
                }
                else {
                    // Set pitches to 1 (default) when the pause menu is NOT active in the hierarchy
                    s.pitch = Mathf.Lerp(s.pitch, 1f, audioLerpSpeed);
                }
            }

        }
    }

    private void QuickRestart () {

        if (Input.GetKey(Keybinds.instance.quickRestart)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }


    public void Complete () {
        lcUI.SetActive(true);
        indi.SetActive(false);
        pmUI.SetActive(false);
        Time.timeScale = 1f;
        Destroy(player);
    }

    public void EndGame () {
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, bgColour, colorLerpSpeed * Time.deltaTime);
        goUI.SetActive(true);
        indi.SetActive(false);
        pmUI.SetActive(false);
        Time.timeScale = 1f;
        Destroy(player);
    }
}
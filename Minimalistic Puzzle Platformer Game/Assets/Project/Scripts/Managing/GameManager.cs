using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    [Header("ASSIGNABLES")]

    public GameObject lcUI;
    public GameObject goUI;
    public GameObject indi;
    public GameObject pmObj;
    public GameObject dindi;
    
    public GameObject player;
    public GameObject env;

    public GameObject audio;

    public static GameManager instance;

    private AudioSource[] sources;
    
    [Range(0f, 1f)] [SerializeField] private float audioLerpSpeed;
    [SerializeField] private float pitchWhenPauseMenuIsActive;


    private SlowMotion m;

    private void Awake () {
        instance = this;
        
        // Get all components in the audio source game object's children
        sources = audio.GetComponentsInChildren<AudioSource>();
        
        // Get Slow Motion Script
        m = FindObjectOfType<SlowMotion>();
    }


    private void Update () {
        QuickRestart();
        ChangeAudioPitches();
    }

    private void ChangeAudioPitches () {
        // Foreach loop for all audio sources in the 'sources' array
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
            
            // If the level complete UI is not null, execute all other code
            if (lcUI != null) {
                if (lcUI.activeInHierarchy) {
                    s.pitch = Mathf.Lerp(s.pitch, 0.1f, audioLerpSpeed);
                }
                else {
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
        goUI.SetActive(false);
        indi.SetActive(false);
        dindi.SetActive(false);
        Destroy(pmObj);
        m.enabled = false;
        Time.timeScale = 1f;
        Destroy(player); 
    }


    public void EndGame () {
        goUI.SetActive(true);
        indi.SetActive(false);
        dindi.SetActive(false);
        Destroy(pmObj);
        Time.timeScale = 1f;
        Destroy(player);
    }
}
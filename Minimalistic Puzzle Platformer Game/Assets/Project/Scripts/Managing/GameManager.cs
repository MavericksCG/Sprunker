using UnityEngine;
using UnityEngine.SceneManagement;
using Sprunker.Universal;
using System;

namespace Sprunker.Managing {
    public class GameManager : MonoBehaviour {

        [Header("Assignables")] 
        public GameObject lcUI;
        public GameObject goUI;
        public GameObject indi;
        public GameObject pmObj;
        public GameObject dindi;

        public GameObject player;

        public GameObject audioObj;

        public static GameManager instance;

        private AudioSource[] sources;

        [Range(0f, 1f)] [SerializeField] private float audioLerpSpeed;
        [SerializeField] private float pitchWhenPauseMenuIsActive;

        [SerializeField] private bool logLastRecordedPosition;

        [HideInInspector] public Vector2 lastRecordedPosition;

        private SlowMotion m;

        [SerializeField] private AnimationCurve pitchCurve = new AnimationCurve();


        private void Awake () {
            instance = this;

            // Get all components in the audio source game object's children
            sources = audioObj.GetComponentsInChildren<AudioSource>();

            // Get Slow Motion Script
            m = FindObjectOfType<SlowMotion>();
        }


        private void Update () {
            QuickRestart();
            ChangeAudioPitches();
            HandleAudioPitchCurve();
        
            if (logLastRecordedPosition) {
                Debug.Log(lastRecordedPosition);
            }
        }

        private void HandleAudioPitchCurve () {
            foreach (AudioSource s in sources) {
                pitchCurve.AddKey(Time.realtimeSinceStartup, s.pitch);
            }
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
                        s.volume = Mathf.Lerp(s.volume, 0.3f, audioLerpSpeed);
                    }
                    else {
                        s.volume = Mathf.Lerp(s.volume, 1f, audioLerpSpeed);
                    }
                }

                // If the game over UI is not null, execute all other code
                if (goUI != null) {
                    if (goUI.activeInHierarchy) {
                        s.volume = Mathf.Lerp(s.volume, 0.3f, audioLerpSpeed);
                    }
                    else {
                        s.volume = Mathf.Lerp(s.volume, 0.3f, audioLerpSpeed);
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
        
        
        public void EnableObj (GameObject objToEnable) {
            if (objToEnable != null) {
                objToEnable.SetActive(true);
            }
            else {
                bool log = true;
                if (log == true) {
                    Debug.Log("There is no object assigned to enable!");
                }
                log = false;

            }
        }
    }
    

}
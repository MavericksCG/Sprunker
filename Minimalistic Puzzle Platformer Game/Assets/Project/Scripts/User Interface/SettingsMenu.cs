using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Sprunker.UserInterface {

    public class SettingsMenu : MonoBehaviour {

        [Header("Variables and References")] 
        [SerializeField] private bool logTargetFramerate;

        [SerializeField] private AudioSource clickSFX;

        private LoadingBar bar;

        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private TextMeshProUGUI framerateText;
      
        private Resolution[] resolutions;


        private void Start () {

            // Set target framerate
            Application.targetFrameRate = 60;

            bar = FindObjectOfType<LoadingBar>();

            resolutions = Screen.resolutions;

            dropdown.ClearOptions();

            List<string> res = new List<string>();

            int currentRes = 0;
            for (int i = 0; i < resolutions.Length; i++) {
                string resOption = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
                res.Add(resOption);

                currentRes = i;
            }

            dropdown.AddOptions(res);
            dropdown.value = currentRes;
            dropdown.RefreshShownValue();
        }

        public void SetChosenScreenResolution (int index) {
            Resolution res = resolutions[index];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }

        public void Fullscreen (bool enableFullscreen) {
            Screen.fullScreen = enableFullscreen;
        }

        public void UseVSync (bool useVSync) {
            if (useVSync) QualitySettings.vSyncCount = 1;
            else QualitySettings.vSyncCount = 0;
        }

        public void SetTargetFramerate (float targetFramerate) {
            Application.targetFrameRate = (int)targetFramerate;
            framerateText.text = Application.targetFrameRate.ToString();
        }


        private void Update () {
            if (Input.GetKeyDown(KeyCode.M)) {
                ReturnToMainMenu();
            }

            if (logTargetFramerate)
                Debug.Log(Application.targetFrameRate);
        }

        private void ReturnToMainMenu () {
            bar.LoadScene(1);
            clickSFX.Play();
        }

    }

}
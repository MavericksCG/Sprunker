using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Sprunker.UserInterface {
    public class LoadingBar : MonoBehaviour {

        [SerializeField] private GameObject loadingCanvas;
        [SerializeField] private Slider bar;

        public void LoadScene (int index) {
            StartCoroutine(LoadSceneAsynchronously(index));
        }

        public void LoadNextScene () {
            StartCoroutine(LoadSceneAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
        }

        public void ReloadCurrentScene () {
            StartCoroutine(LoadSceneAsynchronously(SceneManager.GetActiveScene().buildIndex));
        }

        private IEnumerator LoadSceneAsynchronously (int index) {
            AsyncOperation AO = SceneManager.LoadSceneAsync(index);

            loadingCanvas.SetActive(true);
            while (!AO.isDone) {
                float loadProgress = Mathf.Clamp01(AO.progress / 0.9f);
                bar.value = loadProgress;

                yield return null;
            }
        }
    }
}
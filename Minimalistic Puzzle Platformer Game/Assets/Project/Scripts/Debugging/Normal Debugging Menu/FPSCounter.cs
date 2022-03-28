using UnityEngine;
using TMPro;

namespace Sprunker.Debugging.Standard {

    public class FPSCounter : MonoBehaviour {

        public float updateInterval = 0.5f; // How often should the number update

        float accum = 0.0f;
        int frames = 0;
        float timeleft;
        float fps;

        public TextMeshProUGUI fpsUI;

        [SerializeField] private AnimationCurve fpsCurve = new AnimationCurve();


        private void Start () {
            timeleft = updateInterval;
        }

        private void Update () {

            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            frames++;

            // Interval ended - update GUI text and start new interval
            if (timeleft <= 0.0) {
                // display two fractional digits (f2 format)
                fps = (accum / frames);
                timeleft = updateInterval;
                accum = 0.0f;
                frames = 0;
            }

            // Updating the graph
            fpsCurve.AddKey(Time.realtimeSinceStartup, fps);

            fpsUI.text = "FPS : " + fps.ToString("0");

            if (fps >= 60) {
                fpsUI.color = Color.green;
            }

            if (fps <= 30) {
                fpsUI.color = Color.yellow;
            }

            if (fps <= 15) {
                fpsUI.color = Color.red;
            }
        }

    }
}

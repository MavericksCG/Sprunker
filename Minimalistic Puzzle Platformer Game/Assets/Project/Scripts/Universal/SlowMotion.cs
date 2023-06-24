using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Sprunker.Managing;
using Sprunker.Player;

namespace Sprunker.Universal {

    public class SlowMotion : MonoBehaviour {

        [HideInInspector] public bool slowMotionActive = false;
        public bool usePostProcessingEffects;

        [Header("Effects")] [Space] [SerializeField]
        private PostProcessVolume volume;

        // Effects
        private Bloom b;
        private ChromaticAberration ca;
        private LensDistortion ld;
        private Vignette vg;


        [Header("Desired Values")] [SerializeField]
        private float desiredBloomValue;

        [SerializeField] private float desiredAbberationValue;
        [SerializeField] private float desiredDistortionValue;
        [SerializeField] private float desiredVignetteValue;


        [Header("Smoothing")] [SerializeField] [Range(0f, 1f)]
        private float bloomSmoothing;

        [SerializeField] [Range(0f, 1f)] private float abberationSmoothing;
        [SerializeField] [Range(0f, 1f)] private float distortionSmoothing;
        [SerializeField] [Range(0f, 1f)] private float vignetteSmoothing;
        [SerializeField] [Range(0f, 1f)] private float timeSmoothing;

        private PlayerController pc;

        private void Start () {
            volume.profile.TryGetSettings(out b);
            volume.profile.TryGetSettings(out ca);
            volume.profile.TryGetSettings(out ld);
            volume.profile.TryGetSettings(out vg);
            
            // Get Player Controller Script
            pc = FindObjectOfType<PlayerController>();
        }

        private void Update () {
            if (Input.GetKey(Keybinds.instance.slowMotion) || Input.GetKey(KeyCode.Quote)) {
                EnterSlowMotion();
            }
            else {
                ExitSlowMotion();       
            }
        }

        private void EnterSlowMotion () {
            slowMotionActive = true;

            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.2f, timeSmoothing);
            b.intensity.value = Mathf.Lerp(b.intensity.value, desiredBloomValue, bloomSmoothing);
            ca.intensity.value = Mathf.Lerp(ca.intensity.value, desiredAbberationValue, abberationSmoothing);
            ld.intensity.value = Mathf.Lerp(ld.intensity.value, desiredDistortionValue, distortionSmoothing);
            vg.intensity.value = Mathf.Lerp(vg.intensity.value, desiredVignetteValue, vignetteSmoothing);
        }

        private void ExitSlowMotion () {
            slowMotionActive = false;

            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, timeSmoothing);
            b.intensity.value = Mathf.Lerp(b.intensity.value, 2f, bloomSmoothing);
            ca.intensity.value = Mathf.Lerp(ca.intensity.value, .02f, abberationSmoothing);
            ld.intensity.value = Mathf.Lerp(ld.intensity.value, 0f, distortionSmoothing);
            vg.intensity.value = Mathf.Lerp(vg.intensity.value, 0.136f, vignetteSmoothing);
        }
    }
}
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SlowMotion : MonoBehaviour {

    /* 
        Bloom - Intensity = 2 Threshold = 0.95
        Vignette - Intensity = 0.136
        Lens Distortion - Intensity = 0
        Chromatic Abberation - 0.02 
    */

    [HideInInspector] public bool slowMotionActive;


    [Header("EFFECTS")] [Space]
    public PostProcessVolume volume;

    // Effects
    private Vignette vg;
    private Bloom b;
    private ChromaticAberration ca;
    private LensDistortion ld;


    [Header("DESIRED VALUES")] [Space]
    public float desiredVignetteAmount;
    public float desiredBloomIntensityAmount;
    public float desiredChromaticAbberationAmount;
    public float desiredLensDistortionAmount;


    [Header("SMOOTHING")]
    [Space]
    [Range(0f, 1f)] public float vignetteSmoothSpeed;
    [Range(0f, 1f)] public float bloomSmoothSpeed;
    [Range(0f, 1f)] public float chromaticAbberationSmoothSpeed;
    [Range(0f, 1f)] public float lensDistortionSmoothSpeed;

    [Range(0f, 1f)] public float timeScaleSmoothing;


    [Header("REVERSE SMOOTHING")] [Space]
    [Range(0f, 1f)] public float reverseVignetteSmoothSpeed;
    [Range(0f, 1f)] public float reverseBloomSmoothSpeed;
    [Range(0f, 1f)] public float revereChromaticAbberationSmoothSpeed;
    [Range(0f, 1f)] public float reverseLensDistortionSmoothSpeed;


    private void Update() {
        SlowMotionEnableDisable();
    }

    private void SlowMotionEnableDisable() {

        if (Input.GetKey(Keybinds.instance.slowMotion) && volume.profile.TryGetSettings(out b) && volume.profile.TryGetSettings(out vg) && volume.profile.TryGetSettings(out ca) && volume.profile.TryGetSettings(out ld)) {

            slowMotionActive = true;

            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.2f, timeScaleSmoothing);

            vg.intensity.value = Mathf.Lerp(vg.intensity.value, desiredVignetteAmount, vignetteSmoothSpeed);
            b.intensity.value = Mathf.Lerp(b.intensity.value, desiredBloomIntensityAmount, bloomSmoothSpeed);
            ca.intensity.value = Mathf.Lerp(ca.intensity.value, desiredChromaticAbberationAmount, chromaticAbberationSmoothSpeed);
            ld.intensity.value = Mathf.Lerp(ld.intensity.value, desiredLensDistortionAmount, lensDistortionSmoothSpeed);

        }
        else if (volume.profile.TryGetSettings(out b) && volume.profile.TryGetSettings(out vg) && volume.profile.TryGetSettings(out ca) && volume.profile.TryGetSettings(out ld)) {

            slowMotionActive = false;

            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, timeScaleSmoothing);

            vg.intensity.value = Mathf.Lerp(vg.intensity.value, .136f, reverseVignetteSmoothSpeed);
            b.intensity.value = Mathf.Lerp(b.intensity.value, 2f, reverseBloomSmoothSpeed);
            ca.intensity.value = Mathf.Lerp(ca.intensity.value, 0.02f, revereChromaticAbberationSmoothSpeed);
            ld.intensity.value = Mathf.Lerp(ld.intensity.value, 0f, reverseLensDistortionSmoothSpeed);

        }
    }
}
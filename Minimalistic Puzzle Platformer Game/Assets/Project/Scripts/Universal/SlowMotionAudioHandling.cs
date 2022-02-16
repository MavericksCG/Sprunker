using UnityEngine;

public class SlowMotionAudioHandling : MonoBehaviour {

    [Header("References & Variables")]
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource superJump;
    [SerializeField] private AudioSource soundtrack;

    [Range(0f, 1f)] [SerializeField] private float audioLerpSpeed;

    [SerializeField] private float pitchInSlowMotion = 0.1f;
    [SerializeField] private float normalPitch = 1f;

    private SlowMotion slowMotion;


    private void Awake () {
        slowMotion = FindObjectOfType<SlowMotion>();
    }

    private void Update () {
        if (slowMotion.slowMotionActive) { 
            jump.pitch = Mathf.Lerp(jump.pitch, pitchInSlowMotion, audioLerpSpeed);
            superJump.pitch = Mathf.Lerp(superJump.pitch, pitchInSlowMotion, audioLerpSpeed);
            soundtrack.pitch = Mathf.Lerp(soundtrack.pitch, pitchInSlowMotion, audioLerpSpeed);
        }
        else {
            jump.pitch = Mathf.Lerp(jump.pitch, normalPitch, audioLerpSpeed);
            superJump.pitch = Mathf.Lerp(superJump.pitch, normalPitch, audioLerpSpeed);
            soundtrack.pitch = Mathf.Lerp(soundtrack.pitch, normalPitch, audioLerpSpeed);
        }
    }

}
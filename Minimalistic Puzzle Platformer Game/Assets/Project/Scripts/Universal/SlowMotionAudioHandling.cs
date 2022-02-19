using System;
using UnityEngine;

public class SlowMotionAudioHandling : MonoBehaviour {

    private AudioSource[] sources;

    private SlowMotion slowMotion;

    [SerializeField] private float pitchInSlowMotion = 0.1f;
    [Range(0f, 1f)] [SerializeField] private float audioLerpSpeed = 0.01f;

    private void Awake () {
        // Get All Audio Source Components in the children.
        sources = GetComponentsInChildren<AudioSource>();
        // Get Slow Motion.
        slowMotion = FindObjectOfType<SlowMotion>();
    }

    private void Update () {
        foreach (AudioSource s in sources) {
            if (slowMotion.slowMotionActive) {
                s.pitch = Mathf.Lerp(s.pitch, pitchInSlowMotion, audioLerpSpeed);
            }
            else {
                s.pitch = Mathf.Lerp(s.pitch, 1f, audioLerpSpeed);
            }
        }
    }
}
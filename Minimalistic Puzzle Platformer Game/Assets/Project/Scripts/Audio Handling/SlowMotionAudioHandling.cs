using System;
using UnityEngine;
using Sprunker.Universal;

namespace Sprunker.Audio.Handling {
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
            // Loop through each audio source in the sources array
            foreach (AudioSource s in sources) {
                // Check if slow motion is active
                if (slowMotion.slowMotionActive) {
                    // Change all pitches to pitchInSlowMotion
                    s.pitch = Mathf.Lerp(s.pitch, pitchInSlowMotion, audioLerpSpeed);
                }
                else {
                    // Change all pitches to 1(default)
                    s.pitch = Mathf.Lerp(s.pitch, 1f, audioLerpSpeed);
                }
            }
        }
    }
}
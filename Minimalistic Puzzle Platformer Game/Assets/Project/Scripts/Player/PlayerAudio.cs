using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Sprunker.Player {
    public class PlayerAudio : MonoBehaviour {

        [Header("General")] [SerializeField] private AudioSource soundtrack;

        [SerializeField] [Range(0.1f, 1f)] private float desiredVolume;
        [SerializeField] private float smoothSpeed;
        private float vel = 0f;

        private PlayerController c;


        private void Awake () {
            c = GetComponent<PlayerController>();
        }

        private void Update () {
            if (!c.IsSprinting()) {
                soundtrack.volume = Mathf.SmoothDamp(soundtrack.volume, desiredVolume, ref vel, smoothSpeed, smoothSpeed);
            }
            else {
                soundtrack.volume = Mathf.SmoothDamp(soundtrack.volume, 0.691f, ref vel, smoothSpeed, smoothSpeed);
            }
        }
    }
}

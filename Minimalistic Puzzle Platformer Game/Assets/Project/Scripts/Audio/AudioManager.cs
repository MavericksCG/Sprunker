using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    [Header("References & Variables")]
    public Sound[] sounds;
    private SlowMotion slowMotion;

    public static AudioManager instance;

    // Slow Motion Handling
    public AudioSource jump;
    public AudioSource superJump;
    public AudioSource gameSoundtrack;

    [Range(0f, 1f)] public float audioLerpSpeed;
    public float pitchInSlowMotion;
    private float normalPitch = 1f;


    /// <summary>
    /// Add the audio source component to the game object and then initialize all the types.
    /// </summary>
    private void Awake () {
        // Find the object of type "Slow Motion"
        slowMotion = FindObjectOfType<SlowMotion>();

        // Don't destroy this game object when a new scene gets loaded
        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) { 

            // Initialize Types
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }


    /// <summary>
    /// Handle Audio Playback
    /// </summary>
    public void PlayAudio (string sourceName) {
        Sound s = Array.Find(sounds, sound => sound.audioName == name);
    
        // Check if the sound is null.
        // If it is null, throw a Debug.LogWarning to warn that it could not be located and then return.
        if (s == null) {
            Debug.LogWarning("Sound name : " + name + " could not be located.");
            return;
        }

        s.source.Play();
    }

    private void Update () {
        if (slowMotion.slowMotionActive) {
            jump.pitch = Mathf.Lerp(jump.pitch, pitchInSlowMotion, audioLerpSpeed);
            superJump.pitch = Mathf.Lerp(superJump.pitch, pitchInSlowMotion, audioLerpSpeed);
            gameSoundtrack.pitch = Mathf.Lerp(gameSoundtrack.pitch, pitchInSlowMotion, audioLerpSpeed);
        }
        else {
            jump.pitch = Mathf.Lerp(jump.pitch, normalPitch, audioLerpSpeed);
            superJump.pitch = Mathf.Lerp(superJump.pitch, normalPitch, audioLerpSpeed);
            gameSoundtrack.pitch = Mathf.Lerp(gameSoundtrack.pitch, normalPitch, audioLerpSpeed);
        }
    }
}

using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

    public string audioName;

    public AudioClip clip;

    public bool loop;
    public bool playOnAwake;

    [Range(0f, 1f)] public float volume;
    [Range(0.1f, 3f)] public float pitch;

    [HideInInspector] public AudioSource source;

}
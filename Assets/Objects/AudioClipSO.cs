using Lean.Transition.Method;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

[System.Serializable]
public class AudioArray
{
    [Header("Audio Clip")]
    public string name = "AudioClip Name";
    public AudioClip audioClip;
    [Header("Volume")]
    [Range(0, 1)]
    public float volume = 1;
    [Header("Pitch")]
    [Range(0, 3)]
    public float pitch = 1;
    [Header("Loop")]
    public bool loop = false;
    [Header("Spatial Blend")]
    [Range(0, 1)]
    public float spatialBlend = 0;
    [Header("Priority")]
    [Range(0, 256)]
    public int priority = 128;
    [Header("Source")]
    public AudioSource source;
    public bool randomizePitch = false;
    public float pitchRange = 0.1f;
    public bool randomizeVolume = false;

    public void Play()
    {
        if (source == null)
        {
            Debug.LogError("AudioSource is null");
            return;
        }

        source.clip = audioClip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.spatialBlend = spatialBlend;
        source.priority = priority;

        if (randomizePitch)
        {
            source.pitch = pitch + Random.Range(-pitchRange, pitchRange);
        }

        if (randomizeVolume)
        {
            source.volume = volume + Random.Range(-pitchRange, pitchRange);
        }

        source.Play();
    }

}

[CreateAssetMenu(fileName = "AudioClip", menuName = "pkmkc/AudioClipSO", order = 1)]
public class AudioClipSO : ScriptableObject
{
    [Header("Name")]
    public string _name = "What the sigma this audio for?";
    public string _desc = "What the sigma this audio for?";
    [Header("Audio Clips")]
    public AudioArray[] audioArray;
    public AudioMixerGroup mixerGroup;
    public LeanAudioSourceVolume volume;
}
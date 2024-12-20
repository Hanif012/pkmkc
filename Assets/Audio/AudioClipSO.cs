using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioArray
{
    [Header("Audio Clip Properties")]
    public string name = "AudioClip Name";
    public AudioClip audioClip;

    [Header("Audio Settings")]
    [Range(0, 1)] public float volume = 1f;
    [Range(0, 3)] public float pitch = 1f;
    public bool loop = false;
    [Range(0, 1)] public float spatialBlend = 0f;
    [Range(0, 256)] public int priority = 128;

    [Header("Audio Source")]
    public AudioSource source;

    [Header("Randomization Settings")]
    public bool randomizePitch = false;
    [Range(0, 1)] public float pitchRange = 0.1f;
    public bool randomizeVolume = false;

    /// <summary>
    /// Configures and plays the audio source with the given properties.
    /// </summary>
    public void Play()
    {
        if (source == null)
        {
            Debug.LogError("AudioSource is null. Please assign an AudioSource.");
            return;
        }

        source.clip = audioClip;
        source.volume = randomizeVolume ? Mathf.Clamp(volume + Random.Range(-pitchRange, pitchRange), 0f, 1f) : volume;
        source.pitch = randomizePitch ? Mathf.Clamp(pitch + Random.Range(-pitchRange, pitchRange), 0f, 3f) : pitch;
        source.loop = loop;
        source.spatialBlend = spatialBlend;
        source.priority = priority;

        source.Play();
    }
}

[CreateAssetMenu(fileName = "AudioClip", menuName = "Audio Management/AudioClipSO", order = 1)]
public class AudioClipSO : ScriptableObject
{
    [Header("Metadata")]
    public string Name = "Audio Clip Name";
    public string description = "Description of the audio clip.";

    [Header("Audio Arrays")]
    public AudioArray[] audioArray;

    [Header("Mixer Group")]
    public AudioMixerGroup mixerGroup;

    // Optional: Add references to helper classes, like LeanAudioSourceVolume, if necessary.
}

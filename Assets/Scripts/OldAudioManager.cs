using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AudioType
{
    SFX,
    Ambient,
    Music
}
public class AudioManager : MonoBehaviour
{
    
    public AudioClipSO audioClipsMusic;
    public AudioClipSO audioClipsSFX;
    public AudioClipSO audioClipsAmbient;

    // Update is called once per frame
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlayAudio(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.SFX:
                // AudioSource.PlayClipAtPoint(audioClips.sfx, transform.position);
                break;
            case AudioType.Ambient:
                // AudioSource.PlayClipAtPoint(audioClips.ambient, transform.position);
                break;
            case AudioType.Music:
                // AudioSource.PlayClipAtPoint(audioClips.music, transform.position);
                break;
        }
    }

    public void SetVolume(AudioType audioType, float volume)
    {
        switch (audioType)
        {
            case AudioType.SFX:
                // audioClipsSFX.volume = volume;
                break;
            case AudioType.Ambient:
                // audioClipsAmbient.volume = volume;
                break;
            case AudioType.Music:
                // audioClips.music.volume = volume;
                break;
        }
    }
}

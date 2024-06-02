using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    [Header("Audio Sources")]
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] private int sourceCount = 5;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Sound Data")]
    public List<SoundData> sounds = new List<SoundData>();

    private void Start()
    {
        for (int i = 0; i < sourceCount; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            audioSources.Add(source);
        }
    }

    public void PlaySound(string soundName)
    {
        SoundData sound = sounds.Find(s => s.soundName == soundName);
        if (sound != null && sound.audioClip != null)
        {
            AudioSource freeSource = audioSources.Find(source => !source.isPlaying);
            if (freeSource != null)
            {
                freeSource.clip = sound.audioClip;
                freeSource.Play();
            }
        }
    }

    public void StopAllSounds()
    {
        foreach (AudioSource source in audioSources)
        {
            source.Stop();
        }
    }
}

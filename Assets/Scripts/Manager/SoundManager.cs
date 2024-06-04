using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundEffect
{
    QuestClick,
    ButtonClick,
    NavClick,
    CharacterJump,
    Damage,
    FireSound,
    KnifeCut,
    Spring,
    Flour,
    Pinball,
    IceCreamThrow,
    IceCreamHit,
    Victory,
    Respawn,


    // İhtiyacınıza göre diğer sound effect'leri buraya ekleyebilirsiniz
}

public class SoundManager : MonoBehaviour
{

    [Header("Audio Sources")]
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] private int sourceCount = 5;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Sound Data")]
    public List<SoundData> sounds = new List<SoundData>();
     private Dictionary<SoundEffect, AudioSource> playingSounds = new Dictionary<SoundEffect, AudioSource>();

    private void Start()
    {
        for (int i = 0; i < sourceCount; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            audioSources.Add(source);
        }
    }

    public void PlaySound(SoundEffect soundEffect)
    {
        SoundData sound = sounds.Find(s => s.soundEffect == soundEffect);
        if (sound != null && sound.audioClip != null)
        {
            AudioSource freeSource = audioSources.Find(source => !source.isPlaying);
            if (freeSource != null)
            {
                freeSource.clip = sound.audioClip;
                freeSource.loop = sound.loop;
                freeSource.Play();

                // Add the sound to the playingSounds dictionary
                if (!playingSounds.ContainsKey(soundEffect))
                {
                    playingSounds.Add(soundEffect, freeSource);
                }
            }
        }
    }

    public void StopSound(SoundEffect soundEffect)
    {
        if (playingSounds.ContainsKey(soundEffect))
        {
            AudioSource source = playingSounds[soundEffect];
            source.Stop();
            playingSounds.Remove(soundEffect);
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

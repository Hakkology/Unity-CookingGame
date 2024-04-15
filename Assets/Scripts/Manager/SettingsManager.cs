using UnityEngine;
using UnityEngine.Audio;  // AudioMixer için gerekli

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }
    public AudioMixer audioMixer;

    public float MusicVolume { get; private set; }
    public float SFXVolume { get; private set; }
    public float MasterVolume { get; private set; }
    public string Language { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Ayrı ses kontrolleri için metodlar
    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetMasterVolume(float volume)
    {
        MasterVolume = volume;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetLanguage(string language)
    {
        Language = language;
        ApplyLanguageSettings();
    }

    private void ApplyLanguageSettings()
    {
        
    }
}

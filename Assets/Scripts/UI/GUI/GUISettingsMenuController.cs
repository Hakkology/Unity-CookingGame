using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Assuming you are using TextMeshPro for the dropdown and sliders
using UnityEngine.Audio;  // Needed for controlling audio volumes

public class GUISettingsMenuController : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider masterVolumeSlider;

    void Start()
    {
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        // Initialize sliders from saved settings
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFX", 0.75f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music", 0.75f);
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master", 0.75f);

        // Apply the initial slider values to the audio mixer
        SetVolume("SFXVolume", sfxVolumeSlider.value);
        SetVolume("MusicVolume", musicVolumeSlider.value);
        SetVolume("MasterVolume", masterVolumeSlider.value);

        // Initialize the dropdown from saved settings
        languageDropdown.value = PlayerPrefs.GetInt("LanguageSetting", 0);
        languageDropdown.onValueChanged.AddListener(delegate { ChangeLanguage(languageDropdown.value); });

        // Add listeners for sliders
        sfxVolumeSlider.onValueChanged.AddListener(value => SetVolume("SFXVolume", value));
        musicVolumeSlider.onValueChanged.AddListener(value => SetVolume("MusicVolume", value));
        masterVolumeSlider.onValueChanged.AddListener(value => SetVolume("MasterVolume", value));
    }

    private void SetVolume(string parameter, float value)
    {
        audioMixer.SetFloat(parameter, Mathf.Log10(value) * 20);  
        PlayerPrefs.SetFloat(parameter + "Volume", value);  
    }

    private void ChangeLanguage(int index)
    {
        // Change the game language based on the dropdown index
        // Assuming you have a method to apply the language setting
        Debug.Log("Language changed to: " + languageDropdown.options[index].text);
        PlayerPrefs.SetInt("LanguageSetting", index);  // Save the language setting
    }
    public void SwitchToMainMenu()
    {
        UIController.GUI.HideCurrentMenu();
        UIController.GUI.ShowMenu(GUIController.MenuType.Pause);
    }
}

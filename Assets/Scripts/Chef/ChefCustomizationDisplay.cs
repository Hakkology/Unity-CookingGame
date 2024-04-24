using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefCustomizationUI : MonoBehaviour
{
    public Dropdown textureDropdown;
    public Dropdown hatDropdown;
    public Dropdown accessoryDropdown;

    public Button saveButton; // Save butonu referansı

    public ChefCustomizationBehaviour chefCustomization;

    void Start()
    {
        PopulateDropdowns();
        LoadPreferences();
        AddListeners();
    }

    void PopulateDropdowns()
    {
        textureDropdown.ClearOptions();
        List<string> textureOptions = new List<string>();
        foreach (var texture in chefCustomization.textureData.chefTextures)
        {
            textureOptions.Add(texture.name);
        }
        textureDropdown.AddOptions(textureOptions);

        // Hat dropdown'u doldur
        hatDropdown.ClearOptions();
        List<string> hatOptions = new List<string>();
        foreach (var hat in chefCustomization.hatData.chefHats)
        {
            hatOptions.Add(hat.name);
        }
        hatDropdown.AddOptions(hatOptions);

        // Accessory dropdown'u doldur
        accessoryDropdown.ClearOptions();
        List<string> accessoryOptions = new List<string>();
        foreach (var accessory in chefCustomization.accessoryData.chefAccessories)
        {
            accessoryOptions.Add(accessory.name);
        }
        accessoryDropdown.AddOptions(accessoryOptions);
    }

    void AddListeners()
    {
        textureDropdown.onValueChanged.AddListener(delegate { UpdateChefAppearance(); });
        hatDropdown.onValueChanged.AddListener(delegate { UpdateChefAppearance(); });
        accessoryDropdown.onValueChanged.AddListener(delegate { UpdateChefAppearance(); });

        saveButton.onClick.AddListener(SavePreferences); // Save butonuna listener ekle
    }

    void UpdateChefAppearance()
    {
        chefCustomization.UpdateChefTexture(textureDropdown.value);
        chefCustomization.UpdateChefHat(hatDropdown.value);
        chefCustomization.UpdateChefAccessory(accessoryDropdown.value);
    }

    void SavePreferences()
    {
        PlayerPrefs.SetInt("TextureIndex", textureDropdown.value);
        PlayerPrefs.SetInt("HatIndex", hatDropdown.value);
        PlayerPrefs.SetInt("AccessoryIndex", accessoryDropdown.value);
        PlayerPrefs.Save();
    }

    void LoadPreferences()
    {
        if (PlayerPrefs.HasKey("TextureIndex"))
        {
            textureDropdown.value = PlayerPrefs.GetInt("TextureIndex");
            UpdateChefAppearance(); // Yüklenen değerle güncelleme yap
        }
        if (PlayerPrefs.HasKey("HatIndex"))
        {
            hatDropdown.value = PlayerPrefs.GetInt("HatIndex");
            UpdateChefAppearance(); // Yüklenen değerle güncelleme yap
        }
        if (PlayerPrefs.HasKey("AccessoryIndex"))
        {
            accessoryDropdown.value = PlayerPrefs.GetInt("AccessoryIndex");
            UpdateChefAppearance(); 
        }
    }
}

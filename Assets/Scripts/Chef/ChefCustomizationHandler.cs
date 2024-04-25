using UnityEngine;

public class ChefCustomizationHandler : MonoBehaviour
{

    public int CurrentTextureIndex { get; private set; }
    public int CurrentHatIndex { get; private set; }
    public int CurrentAccessoryIndex { get; private set; }
    public void SetTextureIndex(int index)
    {
        CurrentTextureIndex = index;
    }

    public void SetHatIndex(int index)
    {
        CurrentHatIndex = index;
    }

    public void SetAccessoryIndex(int index)
    {
        CurrentAccessoryIndex = index;
    }

    public void SavePreferences()
    {
        PlayerPrefs.SetInt("TextureIndex", CurrentTextureIndex);
        PlayerPrefs.SetInt("HatIndex", CurrentHatIndex);
        PlayerPrefs.SetInt("AccessoryIndex", CurrentAccessoryIndex);
        PlayerPrefs.Save();  
    }

    public void LoadPreferences()
    {
        CurrentTextureIndex = PlayerPrefs.GetInt("TextureIndex", 0);  
        CurrentHatIndex = PlayerPrefs.GetInt("HatIndex", 0);
        CurrentAccessoryIndex = PlayerPrefs.GetInt("AccessoryIndex", 0);
    }
}

using UnityEngine;

public class ChefCustomizationOptions : MonoBehaviour
{
    public int CurrentChefImageIndex { get; private set; }
    public int CurrentChefFlagIndex { get; private set; }
    public int CurrentChefCountryIndex { get; private set; }

    public void SetChefImageIndex(int index)
    {
        CurrentChefImageIndex = index;
    }

    public void SetChefFlagIndex(int index)
    {
        CurrentChefFlagIndex = index;
    }

    public void SetChefCountryIndex(int index)
    {
        CurrentChefCountryIndex = index;
    }

    public void SavePreferences()
    {
        PlayerPrefs.SetInt("ChefImageIndex", CurrentChefImageIndex);
        PlayerPrefs.SetInt("ChefFlagIndex", CurrentChefFlagIndex);
        PlayerPrefs.SetInt("ChefCountryIndex", CurrentChefCountryIndex);
        PlayerPrefs.Save();
    }

    public void LoadPreferences()
    {
        CurrentChefImageIndex = PlayerPrefs.GetInt("ChefImageIndex", 0);
        CurrentChefFlagIndex = PlayerPrefs.GetInt("ChefFlagIndex", 0);
        CurrentChefCountryIndex = PlayerPrefs.GetInt("ChefCountryIndex", 0);
    }
}
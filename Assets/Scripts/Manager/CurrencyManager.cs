using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int totalStars;

    private void LoadTotalStars()
    {
        totalStars = PlayerPrefs.GetInt("TotalStars", 0);
    }

    private void SaveTotalStars()
    {
        PlayerPrefs.SetInt("TotalStars", totalStars);
        PlayerPrefs.Save();
    }

    public void AddStars(int stars)
    {
        totalStars += stars;
        SaveTotalStars();
    }

    public bool SpendStars(int stars)
    {
        if (totalStars >= stars)
        {
            totalStars -= stars;
            SaveTotalStars();
            return true;
        }
        return false;
    }

    public int GetTotalStars()
    {
        return totalStars;
    }
}

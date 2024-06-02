using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int totalStars;
    private int totalCoins;

    private void Start() {
        LoadTotalStars();
        LoadTotalCoins();
    }

    // Stars Management
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

    // Coins Management
    private void LoadTotalCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    private void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }

    public void AddCoins(int coins)
    {
        totalCoins += coins;
        SaveTotalCoins();
    }

    public bool SpendCoins(int coins)
    {
        if (totalCoins >= coins)
        {
            totalCoins -= coins;
            SaveTotalCoins();
            return true;
        }
        return false;
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }
}

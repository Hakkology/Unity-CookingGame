using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }
    private Dictionary<string, int> levelStars;

    void Awake()
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

    public void Initialize(List<GameSceneData> gameScenes)
    {
        LoadStars(gameScenes);
    }

    private void LoadStars(List<GameSceneData> gameScenes)
    {
        levelStars = new Dictionary<string, int>();
        foreach (var scene in gameScenes)
        {
            string levelName = scene.sceneName;
            int stars = PlayerPrefs.GetInt(levelName + "_stars", 0);
            levelStars[levelName] = stars;
        }
    }

    public void SaveStars()
    {
        foreach (var entry in levelStars)
        {
            PlayerPrefs.SetInt(entry.Key + "_stars", entry.Value);
        }
    }

    public void UpdateLevelStars(string levelName, int stars)
    {
        if (stars > 3) stars = 3;
        if (levelStars.ContainsKey(levelName))
        {
            levelStars[levelName] = stars;
        }
        else
        {
            levelStars.Add(levelName, stars);
        }
        SaveStars();
    }

    public int GetTotalStars()
    {
        int totalStars = 0;
        foreach (var stars in levelStars.Values)
        {
            totalStars += stars;
        }
        return totalStars;
    }

    public int GetStarsForLevel(string levelName)
    {
        return levelStars.ContainsKey(levelName) ? levelStars[levelName] : 0;
    }
}

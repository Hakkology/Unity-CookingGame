using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class AchievementManager : MonoBehaviour
{
    private Dictionary<string, int> sceneStarCounts = new Dictionary<string, int>();
    private void Start() {
        LoadStarCounts();
    }

    private void LoadStarCounts()
    {
        if (LevelManager.Instance == null || LevelManager.Instance.gameScenes == null)
        {
            Debug.LogError("LevelManager or gameScenes list is not initialized.");
            return;
        }

        List<GameSceneData> gameScenes = LevelManager.Instance.gameScenes;
        foreach (var sceneData in gameScenes)
        {
            string sceneName = sceneData.sceneName;
            int starCount = PlayerPrefs.GetInt(sceneName, 0);
            sceneStarCounts[sceneName] = starCount;
        }
    }

    public int GetStarCount(string sceneName)
    {
        if (sceneStarCounts.TryGetValue(sceneName, out int starCount))
        {
            return starCount;
        }
        return 0;
    }

    public void SetStarCount(string sceneName, int newStarCount)
    {
        if (sceneStarCounts.TryGetValue(sceneName, out int currentStarCount))
        {
            if (newStarCount > currentStarCount)
            {
                int starsToAdd = newStarCount - currentStarCount;
                LevelManager.CurrencyManager.AddStars(starsToAdd);
                sceneStarCounts[sceneName] = newStarCount;
                PlayerPrefs.SetInt(sceneName, newStarCount);
                PlayerPrefs.Save();
            }
        }
        else
        {
            sceneStarCounts[sceneName] = newStarCount;
            PlayerPrefs.SetInt(sceneName, newStarCount);
            PlayerPrefs.Save();
        }
    }

    public void ResetAllStars()
    {
        foreach (var scene in sceneStarCounts.Keys)
        {
            sceneStarCounts[scene] = 0;
            PlayerPrefs.SetInt(scene, 0);
        }
        PlayerPrefs.Save();
    }
}

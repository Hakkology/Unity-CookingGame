using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public GameObject UIControllerPrefab;
    private GameObject instantiatedUIController;
    public event Action<GUIHighScoreController> OnUIReady;
    void OnEnable() => SceneManager.sceneUnloaded += OnSceneUnloaded;
    void OnDisable() => SceneManager.sceneUnloaded -= OnSceneUnloaded;
    
    public void LoadScene(GameState gameState, GameSceneData gameSceneData = null)
    {
        string sceneName = GetSceneNameByGameState(gameState, gameSceneData);
        if (sceneName == null) Debug.Log("Scene not implemented yet.");
        else SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded();
        if (gameState == GameState.Play) SceneManager.sceneLoaded += (scene, mode) => OnPlaySceneLoaded(scene, gameSceneData);
    }

    private void OnSceneLoaded()
    {
        if (instantiatedUIController != null)
        {
            Destroy(instantiatedUIController);
            instantiatedUIController = null;
        }
    }
    private void OnPlaySceneLoaded(Scene scene, GameSceneData gameSceneData)
    {
        LevelManager.InstructionHandler.ResetInstructions();
        LevelManager.InstructionHandler.SetInstructions(gameSceneData.instructions);
        SceneManager.sceneLoaded -= (loadedScene, mode) => OnPlaySceneLoaded(loadedScene, gameSceneData);

        instantiatedUIController = Instantiate(UIControllerPrefab);
        UIController.sceneData = gameSceneData;
        UIController.HUD.PopulateSpawners();
        GUIHighScoreController highScoreController = instantiatedUIController.GetComponentInChildren<GUIHighScoreController>(true);
        if (highScoreController != null)
        {
            OnUIReady?.Invoke(highScoreController);
        }
        else
        {
            Debug.LogError("HighScoreController component not found on instantiated UIController.");
        }

        LevelManager.AchievementHandler.ResetAchievements();
    }

    private string GetSceneNameByGameState(GameState gameState, GameSceneData sceneData = null)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                return "MainMenuScene";
            case GameState.SelectionMenu:
                return "SelectionScene";
            case GameState.CustomizationsMenu:
                return "CustomizationScene";
            case GameState.Play:
                if (sceneData != null && !string.IsNullOrEmpty(sceneData.sceneName)) return sceneData.sceneName;
                else
                {
                    Debug.Log("GameSceneData is null or sceneName is not specified for Play state.");
                    return null; 
                }
            case GameState.Settings:
                return "SettingsScene";
            default:
                throw new System.ArgumentOutOfRangeException("Unknown GameState.");
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (instantiatedUIController != null){
            Destroy(instantiatedUIController); 
            instantiatedUIController = null;
        }  
    }
}

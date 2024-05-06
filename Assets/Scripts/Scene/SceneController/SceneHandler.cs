using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public GameObject UIControllerPrefab;
    public GameObject playerPrefab;
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
        // Reset all instructions to reinstate quests for each scene.
        LevelManager.InstructionHandler.ResetInstructions();
        LevelManager.InstructionHandler.SetInstructions(gameSceneData.instructions);

        // Add gamescenedata to the scene as it loads.
        SceneManager.sceneLoaded -= (loadedScene, mode) => OnPlaySceneLoaded(loadedScene, gameSceneData);

        // Instantiate UI with the game.
        instantiatedUIController = Instantiate(UIControllerPrefab);
        UIController.sceneData = gameSceneData;
        UIController.HUD.PopulateSpawners();

        // Instantiate achievements and highscores with the game.
        GUIHighScoreController highScoreController = instantiatedUIController.GetComponentInChildren<GUIHighScoreController>(true);
        if (highScoreController != null) OnUIReady?.Invoke(highScoreController);
        else Debug.LogError("HighScoreController component not found on instantiated UIController.");
        LevelManager.AchievementHandler.ResetAchievements();

        // Instantiate the player based on customization choices.
        GameObject playerInstance = Instantiate(playerPrefab, gameSceneData.playerSpawnPosition, Quaternion.identity);
        ChefCustomizationBehaviour chefCustomization = playerInstance.GetComponent<ChefCustomizationBehaviour>();
        if (chefCustomization != null) chefCustomization.ResetCharacterToSavedPreferences();
        else Debug.LogError("ChefCustomizationBehaviour not found on the instantiated player.");

        // Set the camera to follow the player.
        CameraBehaviour cameraBehaviour = FindObjectOfType<CameraBehaviour>();
        if (cameraBehaviour != null) cameraBehaviour.SetPlayerTransform(playerInstance.transform);

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

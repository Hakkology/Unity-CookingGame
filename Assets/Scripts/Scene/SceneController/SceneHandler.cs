using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public event Action<GameObject> OnPlayerSpawned;
    public event Action<GUIHighScoreController> OnUIReady;

    public GameObject UIControllerPrefab;
    public GameObject playerPrefab;

    private GameObject instantiatedUIController;
    private GameObject playerInstance;
    private void Awake() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;
    public void LoadScene(GameState gameState, GameSceneData gameSceneData = null)
    {
        string sceneName = GetSceneNameByGameState(gameState, gameSceneData);
        if (sceneName == null) Debug.Log("Scene not implemented yet.");
        else SceneManager.LoadScene(sceneName);

        if (gameState == GameState.Play) SceneManager.sceneLoaded += (scene, mode) => {
            OnGameSceneUnloaded(scene);
            OnPlaySceneLoaded(scene, gameSceneData);
        };
        else SceneManager.sceneLoaded += (scene, mode) => OnGameSceneUnloaded(scene);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnGameSceneUnloaded(scene);
        if (SceneManager.GetActiveScene().name == GetSceneNameByGameState(GameState.Play, UIController.sceneData)) OnPlaySceneLoaded(scene, UIController.sceneData);
    }

    private void OnPlaySceneLoaded(Scene scene, GameSceneData gameSceneData)
    {
        // Reset all instructions to reinstate quests for each scene.
        LevelManager.InstructionHandler.ResetInstructions();
        LevelManager.InstructionHandler.SetInstructions(gameSceneData.instructions);

        // Instantiate UI with the game.
        Debug.Log("UI created again.");
        instantiatedUIController = Instantiate(UIControllerPrefab);
        UIController.sceneData = gameSceneData;
        UIController.HUD.PopulateSpawners();

        // Instantiate achievements and highscores with the game.
        GUIHighScoreController highScoreController = instantiatedUIController.GetComponentInChildren<GUIHighScoreController>(true);
        if (highScoreController != null) OnUIReady?.Invoke(highScoreController);
        else Debug.LogError("HighScoreController component not found on instantiated UIController.");
        LevelManager.AchievementHandler.ResetAchievements();

        // Instantiate the player based on customization choices and update camera info.
        playerInstance = Instantiate(playerPrefab, gameSceneData.playerSpawnPosition, Quaternion.identity);
        if (playerInstance != null) OnPlayerSpawned?.Invoke(playerInstance);
        ChefCustomizationBehaviour chefCustomization = playerInstance.GetComponentInChildren<ChefCustomizationBehaviour>();
        if (chefCustomization != null) chefCustomization.ResetCharacterToSavedPreferences();
        else Debug.LogError("ChefCustomizationBehaviour not found on the instantiated player.");

        // Add gamescenedata to the scene as it loads.
        SceneManager.sceneLoaded -= (loadedScene, mode) => OnPlaySceneLoaded(loadedScene, gameSceneData);
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

    private void OnGameSceneUnloaded(Scene scene)
    {
        if (playerInstance != null){
            Destroy(playerInstance); 
            playerInstance = null;
        }
        if (instantiatedUIController != null){
            Destroy(instantiatedUIController); 
            instantiatedUIController = null;
        }  
        //SceneManager.sceneLoaded -= (scene, mode) => OnGameSceneUnloaded(scene);
    }
}

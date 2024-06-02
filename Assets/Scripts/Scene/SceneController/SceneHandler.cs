using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public event Action<GameObject> OnPlayerSpawned;
    public event Action<GUIHighScoreController> OnUIReady;
    public event Action<GameSceneData> OnPlayScene;

    public GameObject UIControllerPrefab;
    public GameObject playerPrefab;

    private GameObject instantiatedUIController;
    private GameObject playerInstance;

    public void LoadScene(GameState gameState, GameSceneData gameSceneData = null)
    {
        string sceneName = GetSceneNameByGameState(gameState, gameSceneData);
        LevelManager.SoundManager.PlaySound("ButtonClick");
        if (sceneName == null) {
            Debug.Log("Scene not implemented yet."); 
            return;
        }
        StartCoroutine(LoadSceneAsync(gameState, gameSceneData, sceneName));
    }

    public IEnumerator LoadSceneAsync(GameState state, GameSceneData data, string name){

        // LoadingScreen.Show();
        // Make sure ui and player is cleared.
        yield return StartCoroutine(OnGameSceneLoadingAsync());

        // Load scene asynchronously.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        yield return new WaitUntil(() => asyncLoad.isDone);

        // Handle scene adjustments like spawning UI and player.
        if (state == GameState.Play) OnPlaySceneLoaded(data);
    }

    public void OnPlaySceneLoaded(GameSceneData gameSceneData)
    {
        // Add EventSystem if not present
        EnsureEventSystem();

        // Trigger gamescenedata to the game.
        TriggerPlaySceneLoaded(gameSceneData);

        // Reset all instructions to reinstate quests for each scene.
        LevelManager.InstructionHandler.ResetInstructions();
        LevelManager.InstructionHandler.SetInstructions(gameSceneData.instructions);

        // Instantiate UI with the game.
        instantiatedUIController = Instantiate(UIControllerPrefab);
        if (instantiatedUIController != null) Debug.Log("UI prefab instantiation failed.");
        UIController.sceneData = gameSceneData;
        UIController.HUD.PopulateSpawners();

        // Instantiate achievements and highscores with the game.
        GUIHighScoreController highScoreController = instantiatedUIController.GetComponentInChildren<GUIHighScoreController>(true);
        if (highScoreController != null) OnUIReady?.Invoke(highScoreController);
        else Debug.LogError("HighScoreController component not found on instantiated UIController.");

        // Reset achievements and adjust for new level.
        LevelManager.AchievementHandler.ResetAchievements();

        // Instantiate the player based on customization choices and update camera info.
        playerInstance = Instantiate(playerPrefab, gameSceneData.playerSpawnPosition, Quaternion.identity);

        if (playerInstance != null) {
            OnPlayerSpawned?.Invoke(playerInstance);

            // Setting ball respawn handler.
            BallRespawnHandler ballRespawnHandler = playerInstance.GetComponentInChildren<BallRespawnHandler>();
            if (ballRespawnHandler != null)
                ballRespawnHandler.SetRespawn(gameSceneData);
        } 

        // Install chef customization options.
        ChefCustomizationBehaviour chefCustomization = playerInstance.GetComponentInChildren<ChefCustomizationBehaviour>();
        if (chefCustomization != null) chefCustomization.ResetCharacterToSavedPreferences();
        else Debug.LogError("ChefCustomizationBehaviour not found on the instantiated player.");
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

    private IEnumerator OnGameSceneLoadingAsync()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = null;
        }
        if (instantiatedUIController != null)
        {
            Destroy(instantiatedUIController);
            instantiatedUIController = null;
        }
        
        yield return new WaitForEndOfFrame();
    }
    private void EnsureEventSystem()
    {
        if (EventSystem.current == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }
    private void TriggerPlaySceneLoaded(GameSceneData gameSceneData) => OnPlayScene?.Invoke(gameSceneData);
    
}

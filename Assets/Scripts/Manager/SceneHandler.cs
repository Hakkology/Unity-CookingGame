using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void LoadScene(GameState gameState, GameSceneData gameSceneData = null)
    {
        string sceneName = GetSceneNameByGameState(gameState);
        SceneManager.LoadScene(sceneName);
        if (gameState == GameState.Play && gameSceneData != null) SceneManager.sceneLoaded += (scene, mode) => OnPlaySceneLoaded(scene, gameSceneData);
        
    }

    private void OnPlaySceneLoaded(Scene scene, GameSceneData gameSceneData)
    {
        LevelManager.InstructionHandler.SetInstructions(gameSceneData.instructions);
        SceneManager.sceneLoaded -= (loadedScene, mode) => OnPlaySceneLoaded(loadedScene, gameSceneData);
    }

    private string GetSceneNameByGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                return "MainMenu";
            case GameState.SelectionMenu:
                return "SelectionMenu";
            case GameState.CustomizationsMenu:
                return "CustomizationScene";
            case GameState.Play:
                return "PlayScene";
            case GameState.Settings:
                return "SettingsScene";
            default:
                throw new System.ArgumentOutOfRangeException("Unknown GameState.");
        }
    }
}

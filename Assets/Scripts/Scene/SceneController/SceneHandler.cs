using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadScene(GameState gameState, GameSceneData gameSceneData = null)
    {
        string sceneName = GetSceneNameByGameState(gameState, gameSceneData);
        SceneManager.LoadScene(sceneName);
        if (sceneName == null) Debug.Log("Scene not implemented yet.");
        else if (gameState == GameState.Play) SceneManager.sceneLoaded += (scene, mode) => OnPlaySceneLoaded(scene, gameSceneData);
    }

    private void OnPlaySceneLoaded(Scene scene, GameSceneData gameSceneData)
    {
        LevelManager.InstructionHandler.SetInstructions(gameSceneData.instructions);
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
}

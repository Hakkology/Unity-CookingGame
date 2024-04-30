using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectionMenuState
{
    private GameSceneData gameSceneData;
    public void GoToGame() => LevelManager.SceneHandler.LoadScene(GameState.Play, gameSceneData);
    public void GoToCustomizationMenu() => LevelManager.SceneHandler.LoadScene(GameState.CustomizationsMenu);
    public void GotoMainMenu() => LevelManager.SceneHandler.LoadScene(GameState.MainMenu);
}
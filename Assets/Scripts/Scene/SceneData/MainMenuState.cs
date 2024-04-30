using UnityEngine;

public class MainMenuState
{
    public void GoToSelectionMenu() => LevelManager.SceneHandler.LoadScene(GameState.SelectionMenu);
    public void GoToSettingsMenu() => LevelManager.SceneHandler.LoadScene(GameState.Settings);
    public void GoToCustomizationMenu() => LevelManager.SceneHandler.LoadScene(GameState.CustomizationsMenu);
}


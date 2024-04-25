using UnityEngine;

public class MainMenuState
{
    public void GoToSelectionMenu() => SceneHandler.Instance.LoadScene(GameState.SelectionMenu);
    public void GoToSettingsMenu() => SceneHandler.Instance.LoadScene(GameState.Settings);
    public void GoToCustomizationMenu() => SceneHandler.Instance.LoadScene(GameState.CustomizationsMenu);
}


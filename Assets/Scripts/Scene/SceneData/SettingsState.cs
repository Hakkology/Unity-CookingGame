using UnityEngine;

public class SettingsState
{
    public void GoToMainMenu() => LevelManager.SceneHandler.LoadScene(GameState.MainMenu);
}


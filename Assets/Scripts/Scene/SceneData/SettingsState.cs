using UnityEngine;

public class SettingsState
{
    public void GoToMainMenu() => SceneHandler.Instance.LoadScene(GameState.MainMenu);
}


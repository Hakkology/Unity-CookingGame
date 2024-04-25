using UnityEngine;

public class SettingsState : MonoBehaviour
{
    public void GoToMainMenu() => SceneHandler.Instance.LoadScene(GameState.MainMenu);
}


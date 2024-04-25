using UnityEngine;

public class MainMenuState : MonoBehaviour
{
    public void GoToSelectionMenu() => SceneHandler.Instance.LoadScene(GameState.SelectionMenu);
    public void GoToSettingsMenu() => SceneHandler.Instance.LoadScene(GameState.Settings);
}


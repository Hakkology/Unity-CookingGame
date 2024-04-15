using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private MainMenuState mainMenuState;
    private void Awake() => mainMenuState = new MainMenuState();
    public void OnSettingsButtonClicked() => mainMenuState.GoToSettings();
    public void OnSelectionMenuButtonClicked() => mainMenuState.GoToSelectionMenu();
}
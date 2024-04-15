using UnityEngine;

public class SettingsController : MonoBehaviour
{
    private SettingsState settingsState;
    private void Awake() => settingsState = new SettingsState();
    public void OnMainMenuButtonClicked() => settingsState.GotoMainMenu();
}
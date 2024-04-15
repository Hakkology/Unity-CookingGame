using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public RectTransform mainMenu;
    public RectTransform settingsMenu;
    private MainMenuState mainMenuState;
    private void Awake() => mainMenuState = new MainMenuState();
    public void OnSelectionMenuButtonClicked() => mainMenuState.GoToSelectionMenu();
    public void ExitButtonClicked() => Application.Quit();
    public void OpenSettingsMenu() {
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    } 
    public void BackToMainMenu() {
        settingsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public RectTransform mainMenu;
    public RectTransform settingsMenu;
    private MainMenuState mainMenuState;
    public RectTransform settingsButton;
    public RectTransform backButton;
    private void Start() => mainMenuState = new MainMenuState();
    public void OnSelectionMenuButtonClicked() => mainMenuState.GoToSelectionMenu();
    public void OnCustomizationButtonClicked() => mainMenuState.GoToCustomizationMenu();
    public void ExitButtonClicked() => Application.Quit();

    public void OpenSettingsMenu() {
        mainMenu.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        LevelManager.SoundManager.PlaySound("ButtonClick");
        settingsMenu.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    } 
    public void BackToMainMenu() {
        settingsMenu.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        LevelManager.SoundManager.PlaySound("ButtonClick");
        mainMenu.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
    }
}
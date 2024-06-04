using UnityEngine;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    public RectTransform mainMenu;
    public RectTransform settingsMenu;
    public RectTransform informationMenu;
    private MainMenuState mainMenuState;
    public RectTransform settingsButton;
    public RectTransform backButton;
    public RectTransform informationButton;

    private void Start()
    {
        mainMenuState = new MainMenuState();
    }

    public void OnSelectionMenuButtonClicked() => mainMenuState.GoToSelectionMenu();
    public void OnCustomizationButtonClicked() => mainMenuState.GoToCustomizationMenu();
    public void ExitButtonClicked() => Application.Quit();

    public void OpenSettingsMenu()
    {
        mainMenu.DOAnchorPosX(-Screen.width, 0.2f).OnComplete(() =>
        {
            mainMenu.gameObject.SetActive(false);
            settingsButton.gameObject.SetActive(false);
            informationButton.gameObject.SetActive(false);
            settingsMenu.gameObject.SetActive(true);
            settingsMenu.DOAnchorPosX(0, 0.2f);
            backButton.gameObject.SetActive(true);
        });
        LevelManager.SoundManager.PlaySound(SoundEffect.ButtonClick);
    }

    public void OpenInformationPanel()
    {
        mainMenu.DOAnchorPosX(-Screen.width, 0.2f).OnComplete(() =>
        {
            mainMenu.gameObject.SetActive(false);
            settingsButton.gameObject.SetActive(false);
            informationButton.gameObject.SetActive(false);
            informationMenu.gameObject.SetActive(true);
            informationMenu.DOAnchorPosX(0, 0.2f);
        });
        LevelManager.SoundManager.PlaySound(SoundEffect.ButtonClick);
    }

    public void BackToMainMenu()
    {
        settingsMenu.DOAnchorPosX(Screen.width, 0.2f).OnComplete(() =>
        {
            settingsMenu.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
            settingsButton.gameObject.SetActive(true);
            informationButton.gameObject.SetActive(true);
            mainMenu.DOAnchorPosX(0, 0.2f);
        });
        LevelManager.SoundManager.PlaySound(SoundEffect.ButtonClick);
    }

    public void BackToInformationMenu()
    {
        informationMenu.DOAnchorPosX(Screen.width, 0.2f).OnComplete(() =>
        {
            informationMenu.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
            settingsButton.gameObject.SetActive(true);
            informationButton.gameObject.SetActive(true);
            mainMenu.DOAnchorPosX(0, 0.2f);
        });
        LevelManager.SoundManager.PlaySound(SoundEffect.ButtonClick);
    }
}

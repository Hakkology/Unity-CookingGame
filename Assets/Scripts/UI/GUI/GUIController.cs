using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class GUIController : MonoBehaviour
{
    // Enum to identify each GUI menu
    public enum MenuType
    {
        Highscore,
        Pause,
        Quest,
        Settings
    }

    [Header("Menu Panels")]
    [SerializeField] private GameObject highscoreMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject questMenu;
    [SerializeField] private GameObject settingsMenu;

    public GameObject currentActiveMenu;

    void Start()
    {
        DOTween.Init();
    }

    public void ShowMenu(MenuType menuType)
    {
        if (currentActiveMenu != null && currentActiveMenu.activeInHierarchy)
        {
            // If a menu is currently shown, hide it first with an animation
            HideCurrentMenu(() => ActivateMenu(menuType));
            LevelManager.SoundManager.PlaySound(SoundEffect.ButtonClick);
        }
        else
        {
            ActivateMenu(menuType);
        }
    }

    private void ActivateMenu(MenuType menuType)
    {
        GameObject menuToActivate = null;
        switch (menuType)
        {
            case MenuType.Highscore:
                menuToActivate = highscoreMenu;
                break;
            case MenuType.Pause:
                menuToActivate = pauseMenu;
                break;
            case MenuType.Quest:
                menuToActivate = questMenu;
                break;
            case MenuType.Settings:
                menuToActivate = settingsMenu;
                break;
        }

        if (menuToActivate != null)
        {
            currentActiveMenu = menuToActivate;
            currentActiveMenu.SetActive(true);
            currentActiveMenu.transform.DOScale(1.1f, 0.25f).From(0.9f).SetEase(Ease.OutBack);
        }
    }

    public void HideCurrentMenu(Action onComplete = null)
    {
        if (currentActiveMenu != null)
        {
            currentActiveMenu.transform.DOScale(0.0f, 0.25f).OnComplete(() => {
                currentActiveMenu.SetActive(false);
                onComplete?.Invoke();
            }).SetEase(Ease.InBack);
        }
    }

    public bool IsMenuActive(MenuType menuType)
    {
        switch (menuType)
        {
            case MenuType.Highscore:
                return highscoreMenu.activeSelf;
            case MenuType.Pause:
                return pauseMenu.activeSelf;
            case MenuType.Quest:
                return questMenu.activeSelf;
            case MenuType.Settings:
                return settingsMenu.activeSelf;
            default:
                return false;
        }
    }
}

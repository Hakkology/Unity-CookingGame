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

    private GameObject currentActiveMenu;

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
        }
        else
        {
            ActivateMenu(menuType);
        }
    }

    private void ActivateMenu(MenuType menuType)
    {
        switch (menuType)
        {
            case MenuType.Highscore:
                currentActiveMenu = highscoreMenu;
                break;
            case MenuType.Pause:
                currentActiveMenu = pauseMenu;
                break;
            case MenuType.Quest:
                currentActiveMenu = questMenu;
                break;
            case MenuType.Settings:
                currentActiveMenu = settingsMenu;
                break;
        }

        currentActiveMenu.SetActive(true);
        currentActiveMenu.transform.DOScale(1.1f, 0.25f).From(0.9f).SetEase(Ease.OutBack);
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
}

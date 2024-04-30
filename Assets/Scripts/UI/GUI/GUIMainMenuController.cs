using UnityEngine;

public class GUIMainMenuController : MonoBehaviour
{
    [SerializeField] private GUIController GUIController;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    // This method switches to the settings menu
    public void SwitchToSettingsMenu()
    {
        UIController.GUI.HideCurrentMenu();
        UIController.GUI.ShowMenu(GUIController.MenuType.Settings);
    }
    public void ShowMainMenu() => GUIController.ShowMenu(GUIController.MenuType.Pause);
    public void ContinueGame() => GUIController.HideCurrentMenu();
    // This method returns to the main menu from other menus like settings
    public void BackToMainMenu() => LevelManager.SceneHandler.LoadScene(GameState.SelectionMenu);
    // This method quits the game
    public void QuitGame() => Application.Quit();
    
}

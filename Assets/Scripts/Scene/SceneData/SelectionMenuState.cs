using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectionMenuState
{
    private GameSceneData gameSceneData;
    public void GoToGame() => SceneHandler.Instance.LoadScene(GameState.Play, gameSceneData);
    public void GoToCustomizationMenu() => SceneHandler.Instance.LoadScene(GameState.CustomizationsMenu);
    public void GotoMainMenu() => SceneHandler.Instance.LoadScene(GameState.MainMenu);
}
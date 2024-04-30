using UnityEngine;

public class CustomizationState
{
    public void GoToSelectionMenu() => LevelManager.SceneHandler.LoadScene(GameState.SelectionMenu);
}


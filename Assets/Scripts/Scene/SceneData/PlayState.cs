using UnityEngine;

public class PlayState
{
    public void GoToSelectionMenu() => LevelManager.SceneHandler.LoadScene(GameState.SelectionMenu);
}


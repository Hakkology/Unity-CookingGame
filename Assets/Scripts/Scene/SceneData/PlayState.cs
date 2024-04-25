using UnityEngine;

public class PlayState
{
    public void GoToSelectionMenu() => SceneHandler.Instance.LoadScene(GameState.SelectionMenu);
}


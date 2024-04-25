using UnityEngine;

public class CustomizationState
{
    public void GoToSelectionMenu() => SceneHandler.Instance.LoadScene(GameState.SelectionMenu);
}


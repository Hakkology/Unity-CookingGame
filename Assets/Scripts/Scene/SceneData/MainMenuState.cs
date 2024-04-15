using UnityEngine;

public class MainMenuState : IGameState
{
    public void EnterState() => SceneHandler.Instance.ChangeScene(GameState.MainMenu);
    public void ExitState(){}
    public void UpdateState() {}
    public void GoToSelectionMenu() => SceneHandler.Instance.ChangeScene(GameState.SelectionMenu);

}
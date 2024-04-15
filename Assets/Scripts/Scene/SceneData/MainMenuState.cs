public class MainMenuState : IGameState
{
    public void EnterState() => SceneHandler.Instance.ChangeScene(GameState.MainMenu);
    public void ExitState(){}
    public void UpdateState() { /* Input kontrolü */ }
    public void GoToSettings() => SceneHandler.Instance.ChangeScene(GameState.Settings);
    public void GoToSelectionMenu() => SceneHandler.Instance.ChangeScene(GameState.SelectionMenu);

}
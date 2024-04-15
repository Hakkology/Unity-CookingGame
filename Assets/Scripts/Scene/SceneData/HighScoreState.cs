public class HighScoreState : IGameState
{
    public void EnterState(){}
    public void ExitState() { /* Menüyü kaldır */ }
    public void UpdateState() { /* Input kontrolü */ }
    public void GotoSelectionMenu() => SceneHandler.Instance.ChangeScene(GameState.SelectionMenu);
}
public class SettingsState : IGameState
{
    public void EnterState(){}
    public void ExitState() { /* Menüyü kaldır */ }
    public void UpdateState() { /* Input kontrolü */ }
    public void GotoMainMenu() => SceneHandler.Instance.ChangeScene(GameState.MainMenu);
}
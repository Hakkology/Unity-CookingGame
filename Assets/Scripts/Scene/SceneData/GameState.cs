public class GameSceneState : IGameState
{
    public void EnterState()
    {
        
    }

    public void ExitState()
    {
        
    }

    public void UpdateState()
    {
        
    }
    public void GotoSelectionMenu() => SceneHandler.Instance.ChangeScene(GameState.SelectionMenu);
}
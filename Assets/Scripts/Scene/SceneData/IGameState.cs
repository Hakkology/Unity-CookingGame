public interface IGameState
{
    void EnterState();
    void ExitState();
    void UpdateState();
}

public enum GameState{
    MainMenu,
    Settings,
    SelectionMenu,
    Kitchen,
    Menemen,
    ScoreState
}
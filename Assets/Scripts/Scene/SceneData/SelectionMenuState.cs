using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectionMenuState : IGameState
{
    public void EnterState(){}
    public void ExitState() { /* Menüyü kaldır */ }
    public void UpdateState() { /* Input kontrolü */ }
    public void GoToKitchenGame() => SceneHandler.Instance.ChangeScene(GameState.Kitchen);
    public void GotoMainMenu() => SceneHandler.Instance.ChangeScene(GameState.MainMenu);
}
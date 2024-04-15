using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameSceneState highScoreState;
    private void Awake() => highScoreState = new GameSceneState();
    public void OnSelectionMenuButtonClicked() => highScoreState.GotoSelectionMenu();
}
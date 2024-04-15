using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    private HighScoreState highScoreState;
    private void Awake() => highScoreState = new HighScoreState();
    public void OnSelectionMenuButtonClicked() => highScoreState.GotoSelectionMenu();
}
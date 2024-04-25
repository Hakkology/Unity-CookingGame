using UnityEngine;

public class PlayState : MonoBehaviour
{
    public void GoToSelectionMenu() => SceneHandler.Instance.LoadScene(GameState.SelectionMenu);
}


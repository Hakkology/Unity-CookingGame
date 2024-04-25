using UnityEngine;

public class CustomizationState : MonoBehaviour
{
    public void GoToSelectionMenu() => SceneHandler.Instance.LoadScene(GameState.SelectionMenu);
}


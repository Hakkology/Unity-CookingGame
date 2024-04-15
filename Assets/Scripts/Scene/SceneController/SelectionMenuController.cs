using UnityEngine;

public class SelectionMenuController : MonoBehaviour
{
    private SelectionMenuState selectionMenuState;
    private void Awake() => selectionMenuState = new SelectionMenuState();
    public void OnKitchenButtonClicked() => selectionMenuState.GoToKitchenGame();
    public void OnMainMenuButtonClicked() => selectionMenuState.GotoMainMenu();
}
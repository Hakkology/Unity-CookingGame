using UnityEngine;

public class CustomizationController : MonoBehaviour
{
    private CustomizationState customizationState;
    private void Start() => customizationState = new CustomizationState();
    public void OnMainMenuButtonClicked() => customizationState.GoToSelectionMenu();

}
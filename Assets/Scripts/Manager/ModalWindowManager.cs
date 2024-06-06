using UnityEngine;

public class ModalWindowManager : MonoBehaviour {

    [Header("Modal Window Elements")]
    public GameObject modalWindowSceneError;
    public GameObject modalWindowMessagePanel;
    public Transform modalWindowTransform;
    private GameObject modalScenePlaceholder;
    private GameObject modalMessagePlaceholder;

    private void Start() {
        modalScenePlaceholder = Instantiate(modalWindowSceneError, modalWindowTransform);
        modalScenePlaceholder.SetActive(false);
        modalMessagePlaceholder = Instantiate(modalWindowMessagePanel, modalWindowTransform);
        modalMessagePlaceholder.SetActive(false);
    }

    public void ShowErrorPanel() => modalScenePlaceholder.SetActive(true);
    public void ShowInfoPanel() => modalScenePlaceholder.SetActive(true);
    public void ShowMessagePanel(Tool toolData)
    {
        modalMessagePlaceholder.SetActive(true);
        var messageWindowController =  modalMessagePlaceholder.GetComponent<GUIMessageWindowController>();
        messageWindowController.Initialize(toolData);
    }
}
using UnityEngine;

public class ModalWindowManager : MonoBehaviour {

    [Header("Modal Window Elements")]
    public GameObject modalWindowSceneError;
    public Transform modalWindowTransform;
    private GameObject modalScenePlaceholder;

    private void Start() {
        modalScenePlaceholder = Instantiate(modalWindowSceneError, modalWindowTransform);
        modalScenePlaceholder.SetActive(false);
    }

    public void ShowErrorPanel() => modalScenePlaceholder.SetActive(true);
    public void ShowInfoPanel() => modalScenePlaceholder.SetActive(true);
}
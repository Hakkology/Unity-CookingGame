using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for scene management
using UnityEngine.UI;  // Needed for UI components

public class GUIHighScoreController : MonoBehaviour
{
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button backToMenuButton;
    public void RestartGame() => LevelManager.SceneHandler.LoadScene(GameState.Play, UIController.sceneData);
    public void BackToMenu() => SceneManager.LoadScene("SelectionMenuScene");
}


using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for scene management
using UnityEngine.UI;  // Needed for UI components

public class GUIHighScoreController : MonoBehaviour
{
    [SerializeField] private GUIController GUIController;
    public Image[] starImages; // three star images showing completion.
    public Sprite[] starSprites; // sprite 0 is fail star, sprite 1 is success star.
    public Toggle spiceCollected;
    public Toggle onTimeCompletion;
    public Toggle onHealthLoss;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button backToMenuButton;
    public void RestartGame() => LevelManager.SceneHandler.LoadScene(GameState.Play, UIController.sceneData);
    public void BackToMenu() => SceneManager.LoadScene("SelectionScene");
    public void UpdateStarDisplay(int starCount)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = i < starCount ? starSprites[1] : starSprites[0];
        }
    }

    public void SetConditionStatus(bool spice, bool onTime, bool health)
    {
        spiceCollected.isOn = spice;
        onTimeCompletion.isOn = onTime;
        onHealthLoss.isOn = !health; 
    }
}


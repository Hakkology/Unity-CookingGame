using UnityEngine;
using System;

public class AchievementHandler : MonoBehaviour
{
    private GUIHighScoreController highScoreController;
    private bool spiceCollected = false;
    private bool healthLost = false;
    private bool timerReach = false;

    void Start()
    {
        LevelManager.SceneHandler.OnUIReady += SetHighScoreController;
    }

    void OnDestroy()
    {
        LevelManager.SceneHandler.OnUIReady -= SetHighScoreController;
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        if (highScoreController != null)
        {
            UIController.HUD.OnHealthLoss += HandleHealthLoss;
            UIController.HUD.OnTimerCompletion += HandleTimerReach;
            LevelManager.InstructionHandler.OnSpiceCollected += HandleSpiceCollected;
        }
        else
        {
            Debug.LogError("HighScoreController is not set when trying to subscribe to events.");
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (highScoreController != null)
        {
            UIController.HUD.OnHealthLoss -= HandleHealthLoss;
            UIController.HUD.OnTimerCompletion -= HandleTimerReach;
            LevelManager.InstructionHandler.OnSpiceCollected -= HandleSpiceCollected;
        }
    }
    private void SetHighScoreController(GUIHighScoreController controller)
    {
        highScoreController = controller;
        SubscribeToEvents();
    }
    public void ResetAchievements()
    {
        spiceCollected = false;
        healthLost = false;
        timerReach = false;
    }
    private void HandleSpiceCollected() => spiceCollected = true;
    private void HandleHealthLoss() => healthLost = true;
    private void HandleTimerReach() => timerReach = true;
    public void CheckAchievements()
    {
        if (highScoreController == null)
        {
            Debug.LogError("HighScoreController is not available to update achievements.");
            return;
        }
        int starCount = 3;

        if (timerReach) starCount--;
        if (!spiceCollected) starCount--;
        if (healthLost) starCount--;

        starCount = Mathf.Max(starCount, 0);
        highScoreController.UpdateStarDisplay(starCount);
        highScoreController.SetConditionStatus(spiceCollected, !timerReach, healthLost);
    }
}

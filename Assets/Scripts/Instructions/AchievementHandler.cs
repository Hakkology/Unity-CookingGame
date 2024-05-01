using UnityEngine;
using System;

public class AchievementHandler : MonoBehaviour
{
    public Ingredient specialSpiceIngredient; // Set this in the Inspector to specify the special spice

    public event Action<int> OnStarsAwarded;
    public event Action<string> OnStatusReported; // Event to report status to UI or logs

    private bool spiceCollected = false;
    private bool healthLost = false;
    private bool timerReach = false;

    void Start()
    {
        // Subscribe to necessary events
        LevelManager.InstructionHandler.OnSpiceCollected += HandleSpiceCollected;
        UIController.HUD.OnHealthLoss += HandleHealthLoss;
        UIController.HUD.OnTimerCompletion += HandleTimerReach;
    }

    void OnDestroy()
    {
        // Unsubscribe from events
        LevelManager.InstructionHandler.OnSpiceCollected -= HandleSpiceCollected;
        UIController.HUD.OnHealthLoss -= HandleHealthLoss;
        UIController.HUD.OnTimerCompletion -= HandleTimerReach;
    }

    private void HandleSpiceCollected()
    {
        spiceCollected = true;
        ReportStatus("Special spice has been collected!");
    }

    private void HandleHealthLoss()
    {
        healthLost = true;
        ReportStatus("Health has been lost!");
    }

    private void HandleTimerReach()
    {
        timerReach = true;
        ReportStatus("Timer is exceeded.");
    }

    public void CheckAchievements()
    {
        int starCount = 3;

        // Check if the max time has passed
        if (!timerReach)
        {
            starCount--;
            ReportStatus("Time exceeded. Reducing stars.");
        }

        // Check if the special spice ingredient has been collected
        if (!spiceCollected)
        {
            starCount--;
            ReportStatus("Spice not collected. Reducing stars.");
        }

        // Check if the player has lost any HP
        if (healthLost)
        {
            starCount--;
            ReportStatus("HP lost. Reducing stars.");
        }

        // Adjust for the case when all conditions fail
        if (starCount < 1) starCount = 0;

        Debug.Log($"Stars Awarded: {starCount}");
        OnStarsAwarded?.Invoke(starCount);
        ReportStatus($"Final star count: {starCount}");
    }

    private void ReportStatus(string message)
    {
        Debug.Log(message); // Or update this to report to the game UI
        OnStatusReported?.Invoke(message);
    }
}

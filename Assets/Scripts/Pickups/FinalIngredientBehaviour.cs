using System;
using UnityEngine;

public class FinalIngredientBehaviour : MonoBehaviour
{
    private bool questCompletion = false;

    private void Start()
    {
        LevelManager.InstructionHandler.QuestsCompletion += HandleQuestCompletion;
    }

    private void OnDestroy()
    {
        LevelManager.InstructionHandler.QuestsCompletion -= HandleQuestCompletion;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && questCompletion)
        {
            ReachEnd();
        }
    }

    private void ReachEnd()
    {
        Debug.Log("Game End");
        LevelManager.InstructionHandler.CheckGameCompletion();
        Destroy(gameObject);
    }

    private void HandleQuestCompletion()
    {
        questCompletion = true;
        Debug.Log("All quests complete. You can now finish the game!");
    }
}

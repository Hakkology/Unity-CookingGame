using UnityEngine;

public class IngredientBehaviour : MonoBehaviour, ICollectible, IQuestible
{
    public Ingredient ingredientData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }
    public void CollectItem()
    {
        if (ingredientData.ingredientName == "Spice")
        {
            LevelManager.InstructionHandler.MarkIngredientAsCollected(ingredientData);
            Debug.Log("Spice collected!");
            Destroy(gameObject);
            return;
        }

        Instruction instruction = LevelManager.InstructionHandler.FindInstructionByIngredient(ingredientData);

        // Check if the instruction exists and if the tool is not required or is already collected
        if (instruction != null &&
            (instruction.tool == null || LevelManager.InstructionHandler.IsToolCollected(instruction.tool)))
        {
            if (!LevelManager.InstructionHandler.IsIngredientCollected(ingredientData))
            {
                LevelManager.InstructionHandler.MarkIngredientAsCollected(ingredientData);
                Debug.Log(ingredientData.ingredientName + " collected!");
                UpdateQuest();
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("You need to collect the required tool first!");
        }
    }

    public void UpdateQuest()
    {
        if (ingredientData.ingredientName != "Spice")
        {
            LevelManager.InstructionHandler.UpdateQuestStatus(ingredientData);
        }
    }
}

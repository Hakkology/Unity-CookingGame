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
        Instruction instruction = LevelManager.InstructionHandler.FindInstructionByIngredient(ingredientData);
        
        if (instruction != null && 
            (!instruction.tool || LevelManager.InstructionHandler.IsToolCollected(instruction.tool)))
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
        LevelManager.InstructionHandler.UpdateQuestStatus(ingredientData);
    }
}

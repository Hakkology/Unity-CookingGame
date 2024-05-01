using UnityEngine;
using System.Collections.Generic;
using System;

public class InstructionHandler : MonoBehaviour
{
    public event Action InstructionUpdated;
    public event Action QuestsCompletion;
    public event Action OnSpiceCollected;

    [SerializeField] private List<Instruction> instructions;

    private HashSet<Tool> collectedTools = new HashSet<Tool>();
    private HashSet<Ingredient> collectedIngredients = new HashSet<Ingredient>();
    private Dictionary<Instruction, bool> instructionCompletionStatus;
    private bool questCompletion;

    private void Start(){
        //InitializeCollections();
        InitializeInstructionStatus();
    }

    private void InitializeInstructionStatus(){

        instructionCompletionStatus = new Dictionary<Instruction, bool>();
        foreach (var instruction in instructions)
        {
            instructionCompletionStatus.Add(instruction, false);
        }
        InstructionUpdated?.Invoke();
    }

    public void SetInstructions(Instruction[] newInstructions)
    {
        instructions = new List<Instruction>(newInstructions);
        InitializeInstructionStatus();
        InstructionUpdated?.Invoke();
    }
    public void ResetInstructions()
    {
        collectedTools.Clear();
        collectedIngredients.Clear();
        instructionCompletionStatus.Clear();
        questCompletion = false;
        InstructionUpdated?.Invoke();
    }

    public void MarkToolAsCollected(Tool tool)
    {
        collectedTools.Add(tool);
        UIController.HUD.RefreshSpawners();
        InstructionUpdated?.Invoke();
    }

    public void MarkIngredientAsCollected(Ingredient ingredient)
    {
        if (ingredient.ingredientName == "Spice")
        {
            OnSpiceCollected?.Invoke();
            return;
        } 

        collectedIngredients.Add(ingredient);
        UIController.HUD.RefreshSpawners();
        InstructionUpdated?.Invoke();
    }
    public bool IsIngredientCollected(Ingredient ingredient) => collectedIngredients.Contains(ingredient);
    public bool IsToolCollected(Tool tool) => collectedTools.Contains(tool);
    public Instruction FindInstructionByIngredient(Ingredient ingredient)
    {
        foreach (var instruction in instructions)
        {
            if (instruction.ingredient == ingredient)
                return instruction;
        }
        return null;
    }

    public void UpdateQuestStatus(Ingredient ingredient)
    {
        foreach (var instruction in instructions)
        {
            if (instruction.ingredient == ingredient && !instructionCompletionStatus[instruction])
            {
                instructionCompletionStatus[instruction] = true;
                Debug.Log("Quest updated: " + instruction.description);
                InstructionUpdated?.Invoke();
            }
        }
        CheckAllQuestsCompletion();
    }

    public void CheckAllQuestsCompletion()
    {
        foreach (var pair in instructionCompletionStatus)
        {
            if (!pair.Value && !pair.Key.isFinalInstruction)
            {
                return; 
            }
        }
        Debug.Log("All preliminary quests completed!");
        QuestsCompletion?.Invoke(); // This should be triggered if all non-final quests are completed
        questCompletion = true;
    }

    public void CheckGameCompletion()
    {
        if (questCompletion)
        {
            LevelManager.AchievementHandler.CheckAchievements();
            UIController.GUI.ShowMenu(GUIController.MenuType.Highscore);
        }
    }

    public List<Instruction> GetInstructions() => instructions;
    public bool GetInstructionCompletionStatus(Instruction instruction) => instructionCompletionStatus.ContainsKey(instruction) ? instructionCompletionStatus[instruction] : false;

}

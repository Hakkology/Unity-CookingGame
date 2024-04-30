using UnityEngine;
using System.Collections.Generic;
using System;

public class InstructionHandler : MonoBehaviour
{
    [SerializeField] private List<Instruction> instructions;

    private HashSet<Tool> collectedTools = new HashSet<Tool>();
    private HashSet<Ingredient> collectedIngredients = new HashSet<Ingredient>();
    private Dictionary<Instruction, bool> instructionCompletionStatus;

    private void Start(){
        InitializeCollections();
        InitializeInstructionStatus();
    }

    private void InitializeCollections()
    {
        foreach (Instruction instruction in instructions)
        {
            if (instruction.tool != null && !collectedTools.Contains(instruction.tool))
            {
                collectedTools.Add(instruction.tool);
            }

            if (instruction.ingredient != null && !collectedIngredients.Contains(instruction.ingredient))
            {
                collectedIngredients.Add(instruction.ingredient);
            }
        }
    }

    private void InitializeInstructionStatus(){

        instructionCompletionStatus = new Dictionary<Instruction, bool>();
        foreach (var instruction in instructions)
        {
            instructionCompletionStatus.Add(instruction, false);
        }
    }

    public void SetInstructions(Instruction[] newInstructions)
    {
        instructions = new List<Instruction>(newInstructions);
        InitializeCollections();
        InitializeInstructionStatus();
    }

    public void MarkToolAsCollected(Tool tool)
    {
        collectedTools.Add(tool);
    }

    public void MarkIngredientAsCollected(Ingredient ingredient)
    {
        collectedIngredients.Add(ingredient);
    }

    public bool IsIngredientCollected(Ingredient ingredient)
    {
        return collectedIngredients.Contains(ingredient);
    }

    public bool IsToolCollected(Tool tool)
    {
        return collectedTools.Contains(tool);
    }

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
            }
        }
        CheckAllQuestsCompletion();
    }
    public void CheckAllQuestsCompletion()
    {
        foreach (var status in instructionCompletionStatus.Values)
        {
            if (!status)
            {
                return;
            }
        }
        Debug.Log("All quests completed!");
    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "Instruction", menuName = "Cuisine/Instruction")]
public class Instruction : ScriptableObject
{
    public Ingredient ingredient;
    public Tool tool;
    public string description;
}
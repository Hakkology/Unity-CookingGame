using UnityEngine;

[CreateAssetMenu(fileName = "Cuisine", menuName = "Cuisine/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite ingredientIcon;
}
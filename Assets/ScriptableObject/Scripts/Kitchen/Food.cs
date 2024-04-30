using UnityEngine;

[CreateAssetMenu(fileName = "Dish", menuName = "Cuisine/Food")]
public class Food : ScriptableObject
{
    public string dishName;
    public string description;
    public Sprite icon;
    public Ingredient[] ingredients;
    public Tool[] tools;
    public GameSceneData sceneData;
}
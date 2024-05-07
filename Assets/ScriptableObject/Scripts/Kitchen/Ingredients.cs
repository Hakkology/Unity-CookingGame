using UnityEngine;

[CreateAssetMenu(fileName = "Cuisine", menuName = "Cuisine/Ingredient")]
public class Ingredient : Pickup
{
    public string ingredientName;
    public Sprite ingredientIcon;
    public GameObject ingredientObject;
}
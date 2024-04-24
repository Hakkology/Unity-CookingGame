using UnityEngine;
[CreateAssetMenu(fileName = "Cuisine", menuName = "Cuisine/Kitchen")]
public class Cuisine : ScriptableObject
{
    public string cuisineName;
    public Sprite chefImage;
    public Sprite flag;
    public string description;
    public Food[] foods;
}
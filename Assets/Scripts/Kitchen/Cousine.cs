using UnityEngine;
[CreateAssetMenu(fileName = "Cuisine", menuName = "Cuisine/New Cuisine")]
public class Cuisine : ScriptableObject
{
    public string cuisineName;
    public Sprite chefImage;
    public Sprite flag;
    public string description;
}
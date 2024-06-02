using UnityEngine;

[CreateAssetMenu(fileName = "ChefHatData", menuName = "Customization/ChefHatData")]
public class ChefHatData : ScriptableObject
{
    public string chefHatName;
    public GameObject chefHatObject;
    public Sprite chefHatIcon;
    public int starCost;
    public int coinCost;
}
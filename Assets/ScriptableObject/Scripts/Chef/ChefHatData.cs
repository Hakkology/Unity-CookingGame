using UnityEngine;

[CreateAssetMenu(fileName = "ChefHatData", menuName = "Customization/ChefHatData")]
public class ChefHatData : ScriptableObject
{
    public string chefHatName;
    public GameObject chefHatObject;
    public Sprite chefHatIcon;
}

[CreateAssetMenu(fileName = "ChefHatDataList", menuName = "Customization/ChefHatDataList")]
public class ChefHatDataList : ScriptableObject
{
    public ChefHatData[] chefHatDataList;
}
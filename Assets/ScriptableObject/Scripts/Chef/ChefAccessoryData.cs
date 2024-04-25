using UnityEngine;

[CreateAssetMenu(fileName = "ChefAccessoryData", menuName = "Customization/ChefAccessoryData")]
public class ChefAccessoryData : ScriptableObject
{
    public string chefAccessoryName;
    public GameObject chefAccessoryObject;
    public Sprite chefAccessoryIcon;
}

[CreateAssetMenu(fileName = "ChefAccessoryDataList", menuName = "Customization/ChefAccessoryDataList")]
public class ChefAccessoryDataList : ScriptableObject
{
    public ChefAccessoryData[] chefAccessoryDataList;
}
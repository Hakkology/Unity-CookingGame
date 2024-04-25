using UnityEngine;

[CreateAssetMenu(fileName = "ChefAccessoryData", menuName = "Customization/ChefAccessoryData")]
public class ChefAccessoryData : ScriptableObject
{
    public string chefAccessoryName;
    public GameObject chefAccessoryObject;
    public Sprite chefAccessoryIcon;
}
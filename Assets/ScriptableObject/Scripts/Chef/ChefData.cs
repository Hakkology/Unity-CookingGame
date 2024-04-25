using UnityEngine;

[CreateAssetMenu(fileName = "ChefData", menuName = "Customization/ChefData")]
public class ChefData : ScriptableObject
{
    public ChefAccessoryData chefAccessory;
    public ChefHatData chefHat;
    public ChefTextureData chefTexture;
}
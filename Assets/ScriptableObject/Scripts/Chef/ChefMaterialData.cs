using UnityEngine;

[CreateAssetMenu(fileName = "ChefTextureData", menuName = "Customization/ChefMaterialData")]
public class ChefMaterialData : ScriptableObject
{
    public string chefMaterialName;
    public Material chefMaterial; // Material nesnesine dönüştürdük
    public Sprite chefMaterialIcon;
}
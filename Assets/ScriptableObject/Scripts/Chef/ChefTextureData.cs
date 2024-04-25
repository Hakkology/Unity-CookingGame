using UnityEngine;

[CreateAssetMenu(fileName = "ChefTextureData", menuName = "Customization/ChefTextureData")]
public class ChefTextureData : ScriptableObject
{
    public string chefTextureName;
    public Texture chefTexture;
    public Sprite chefTextureIcon;
}

[CreateAssetMenu(fileName = "ChefTextureDataList", menuName = "Customization/ChefTextureDataList")]
public class ChefTextureDataList : ScriptableObject
{
    public ChefTextureData[] chefTextureDataList;
}
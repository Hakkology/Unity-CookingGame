using UnityEngine;

public class ChefCustomizationBehaviour : MonoBehaviour
{
    public ChefTextureData textureData;
    public ChefHatData hatData;
    public ChefAccessoryData accessoryData;
    
    private void Start()
    {
        var chefCustomizationHandler = ChefCustomizationHandler.Instance;
        if (chefCustomizationHandler != null)
        {
            ApplyCustomizations(chefCustomizationHandler);
        }
    }

    public void UpdateChefTexture(int textureIndex)
    {
        var texture = textureData.chefTextures[textureIndex];
        ChefCustomizationHandler.Instance.ChangeChefTexture(texture, textureIndex);
    }

    public void UpdateChefHat(int hatIndex)
    {
        var hat = hatData.chefHats[hatIndex];
        ChefCustomizationHandler.Instance.ChangeHat(hat, hatIndex);
    }

    public void UpdateChefAccessory(int accessoryIndex)
    {
        var accessory = accessoryData.chefAccessories[accessoryIndex];
        ChefCustomizationHandler.Instance.ChangeFacialHair(accessory, accessoryIndex);
    }

    private void ApplyCustomizations(ChefCustomizationHandler handler)
    {
        UpdateChefTexture(handler.CurrentTextureIndex);
        UpdateChefHat(handler.CurrentHatIndex);
        UpdateChefAccessory(handler.CurrentFacialHairIndex);
    }
}

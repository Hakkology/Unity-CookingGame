using UnityEngine;

public class ChefCustomizationBehaviour : MonoBehaviour
{
    public ChefTextureData textureData;
    public ChefHatData hatData;
    public ChefAccessoryData accessoryData;
    
    private void Start()
    {
        var chefCustomizationHandler = LevelManager.ChefCustomizationHandler;
        if (chefCustomizationHandler != null)
        {
            ApplyCustomizations(chefCustomizationHandler);
        }
        var instructionHandler = LevelManager.InstructionHandler;
        if (instructionHandler != null)
        {
            // instructionHandler ile ilgili işlemler yapabilirsiniz
        }
    }

    public void UpdateChefTexture(int textureIndex)
    {
        var texture = textureData.chefTextures[textureIndex];
        LevelManager.ChefCustomizationHandler.ChangeChefTexture(texture, textureIndex);
    }

    public void UpdateChefHat(int hatIndex)
    {
        var hat = hatData.chefHats[hatIndex];
        LevelManager.ChefCustomizationHandler.ChangeHat(hat, hatIndex);
    }

    public void UpdateChefAccessory(int accessoryIndex)
    {
        var accessory = accessoryData.chefAccessories[accessoryIndex];
        LevelManager.ChefCustomizationHandler.ChangeFacialHair(accessory, accessoryIndex);
    }

    private void ApplyCustomizations(ChefCustomizationHandler handler)
    {
        UpdateChefTexture(handler.CurrentTextureIndex);
        UpdateChefHat(handler.CurrentHatIndex);
        UpdateChefAccessory(handler.CurrentFacialHairIndex);
    }
}

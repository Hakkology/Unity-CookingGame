using System;
using UnityEngine;

public class ChefCustomizationBehaviour : MonoBehaviour
{
    public GameObject hatPlaceholder;
    public GameObject accessoryPlaceholder;
    public Renderer chefBodyRenderer;

    public ChefHatDataList hatDataList;
    public ChefAccessoryDataList accessoryDataList;
    public ChefTextureDataList textureDataList;

    private GameObject[] hatInstances;
    private GameObject[] accessoryInstances;
    private Texture[] textures;  

    private void Awake()
    {
        InstantiateFeatures();
        UpdateCharacter();
    }

    private void InstantiateFeatures()
    {
        // Instantiate and deactivate all hats
        hatInstances = new GameObject[hatDataList.chefHatDataList.Length];
        for (int i = 0; i < hatDataList.chefHatDataList.Length; i++)
        {
            var hatData = hatDataList.chefHatDataList[i];
            hatInstances[i] = Instantiate(hatData.chefHatObject, hatPlaceholder.transform);
            hatInstances[i].SetActive(false);
        }

        // Instantiate and deactivate all accessories
        accessoryInstances = new GameObject[accessoryDataList.chefAccessoryDataList.Length];
        for (int i = 0; i < accessoryDataList.chefAccessoryDataList.Length; i++)
        {
            var accessoryData = accessoryDataList.chefAccessoryDataList[i];
            accessoryInstances[i] = Instantiate(accessoryData.chefAccessoryObject, accessoryPlaceholder.transform);
            accessoryInstances[i].SetActive(false);
        }

        // Load all textures
        textures = new Texture[textureDataList.chefTextureDataList.Length];
        for (int i = 0; i < textureDataList.chefTextureDataList.Length; i++)
        {
            textures[i] = textureDataList.chefTextureDataList[i].chefTexture;
        }
    }

    public void UpdateCharacter()
    {
        UpdateCharacterTexture(LevelManager.ChefCustomizationHandler.CurrentTextureIndex);
        UpdateCharacterHat(LevelManager.ChefCustomizationHandler.CurrentHatIndex);
        UpdateCharacterAccessory(LevelManager.ChefCustomizationHandler.CurrentAccessoryIndex);
    }

    public void ResetCharacterToSavedPreferences()
    {
        LevelManager.ChefCustomizationHandler.LoadPreferences();
        UpdateCharacter();
    }

    public void SaveCharacterPreferences()
    {
        LevelManager.ChefCustomizationHandler.SavePreferences();
    }


    public void UpdateCharacterTexture(int textureIndex)
    {
        if (textureIndex >= 0 && textureIndex < textures.Length)
        {
            chefBodyRenderer.material.mainTexture = textures[textureIndex];
        }
    }

    public void UpdateCharacterHat(int hatIndex)
    {
        for (int i = 0; i < hatInstances.Length; i++)
        {
            hatInstances[i].SetActive(i == hatIndex);
        }
    }

    public void UpdateCharacterAccessory(int accessoryIndex)
    {
        for (int i = 0; i < accessoryInstances.Length; i++)
        {
            accessoryInstances[i].SetActive(i == accessoryIndex);
        }
    }
}
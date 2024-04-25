using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChefCustomizationDisplay : MonoBehaviour
{
    public Image textureDisplay;
    public Image hatDisplay;
    public Image accessoryDisplay;

    public Button nextTextureButton;
    public Button previousTextureButton;
    public Button nextHatButton;
    public Button previousHatButton;
    public Button nextAccessoryButton;
    public Button previousAccessoryButton;

    public ChefCustomizationBehaviour customizationBehaviour;

    private int currentTextureIndex;
    private int currentHatIndex;
    private int currentAccessoryIndex;

    void Start()
    {
        currentTextureIndex = LevelManager.ChefCustomizationHandler.CurrentTextureIndex;
        currentHatIndex = LevelManager.ChefCustomizationHandler.CurrentHatIndex;
        currentAccessoryIndex = LevelManager.ChefCustomizationHandler.CurrentAccessoryIndex;

        UpdateUI();
        AddButtonListeners();
    }

    private void UpdateUI()
    {
        textureDisplay.sprite = customizationBehaviour.textureDataList.chefTextureDataList[currentTextureIndex].chefTextureIcon;
        hatDisplay.sprite = customizationBehaviour.hatDataList.chefHatDataList[currentHatIndex].chefHatIcon;
        accessoryDisplay.sprite = customizationBehaviour.accessoryDataList.chefAccessoryDataList[currentAccessoryIndex].chefAccessoryIcon;
        
        customizationBehaviour.UpdateCharacter();
    }

    private void AddButtonListeners()
    {
        nextTextureButton.onClick.AddListener(() => ChangeTexture(1));
        previousTextureButton.onClick.AddListener(() => ChangeTexture(-1));
        nextHatButton.onClick.AddListener(() => ChangeHat(1));
        previousHatButton.onClick.AddListener(() => ChangeHat(-1));
        nextAccessoryButton.onClick.AddListener(() => ChangeAccessory(1));
        previousAccessoryButton.onClick.AddListener(() => ChangeAccessory(-1));
    }

    private void ChangeTexture(int direction)
    {
        currentTextureIndex = Mathf.Clamp(currentTextureIndex + direction, 0, customizationBehaviour.textureDataList.chefTextureDataList.Length - 1);
        LevelManager.ChefCustomizationHandler.SetTextureIndex(currentTextureIndex);
        UpdateUI();
    }

    private void ChangeHat(int direction)
    {
        currentHatIndex = Mathf.Clamp(currentHatIndex + direction, 0, customizationBehaviour.hatDataList.chefHatDataList.Length - 1);
        LevelManager.ChefCustomizationHandler.SetHatIndex(currentHatIndex);
        UpdateUI();
    }

    private void ChangeAccessory(int direction)
    {
        currentAccessoryIndex = Mathf.Clamp(currentAccessoryIndex + direction, 0, customizationBehaviour.accessoryDataList.chefAccessoryDataList.Length - 1);
        LevelManager.ChefCustomizationHandler.SetAccessoryIndex(currentAccessoryIndex);
        UpdateUI();
    }
}

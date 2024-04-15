using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenuController : MonoBehaviour
{

    [Header("Cuisine RectTransforms")]
    public RectTransform turkishCousine;
    public RectTransform italianCousine;
    public RectTransform chineseCousine;
    public RectTransform mexicanCousine;
    public RectTransform frenchCousine;
    public RectTransform indianCousine;
    public RectTransform greekCousine;

    [Header("World Elements")]
    public RectTransform worldMap;
    public Cuisine[] cuisines;
    public Image chefImage;
    public Image flagImage;
    public Button confirmButton;
    public TextMeshProUGUI descriptionText;

    [Header("Food Elements")]
    private int currentCuisineIndex = -1;

    private SelectionMenuState selectionMenuState;
    private void Awake() => selectionMenuState = new SelectionMenuState();
    public void OnKitchenButtonClicked() => selectionMenuState.GoToKitchenGame();
    public void OnMainMenuButtonClicked() => selectionMenuState.GotoMainMenu();
    void Start() => ToggleCuisineInfo(false);
    private void ToggleCuisineInfo(bool visible)
    {
        chefImage.gameObject.SetActive(visible);
        flagImage.gameObject.SetActive(visible);
        descriptionText.gameObject.SetActive(visible);
        confirmButton.gameObject.SetActive(visible);
    }

    public void OnFlagSelected(int index)
    {
        if(index >= 0 && index < cuisines.Length)
        {
            currentCuisineIndex = index; // Set the current cuisine index

            // Update UI with selected cuisine data
            chefImage.sprite = cuisines[index].chefImage;
            flagImage.sprite = cuisines[index].flag;
            descriptionText.text = cuisines[index].description;

            ToggleCuisineInfo(true);
        }
    }

    public void OnConfirmButtonClicked()
    {
        if(currentCuisineIndex != -1)
        {
            LoadCuisineMenu(currentCuisineIndex);
            worldMap.gameObject.SetActive(false);
        }
    }

    private void LoadCuisineMenu(int cuisineIndex)
    {
        Debug.Log("Loading menu for cuisine: " + cuisines[cuisineIndex].cuisineName);
    }
}
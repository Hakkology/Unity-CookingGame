using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenuController : MonoBehaviour
{

    [Header("Cuisine Elements")]
    public RectTransform UICousine;
    public TextMeshProUGUI UICousineHeader;
    public Image UICousineFlagImage;
    public Image UICousineChef;
    public TextMeshProUGUI UIFoodHeader;
    public Image UIFoodImage;
    public Button UIFoodButton;
    public GameObject UIFoodTemplate;
    private Cuisine currentCousine;
    // Ingredients and tools are to be implemented later.

    [Header("World Elements")]
    public RectTransform worldMap;
    public Image chefImage;
    public Image flagImage;
    public Button confirmButton;
    public TextMeshProUGUI descriptionText;
    public Cuisine[] cuisines;

    [Header("Food Elements")]
    private int currentCuisineIndex = -1;

    private SelectionMenuState selectionMenuState;
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
        currentCousine = cuisines[cuisineIndex];

        UICousineFlagImage.sprite = currentCousine.flag;
        UICousineHeader.text = currentCousine.cuisineName;
        UICousineChef.sprite = currentCousine.chefImage;

        // FoodList GameObject'i bul
        Transform foodListTransform = UICousine.Find("FoodList");
        if (foodListTransform == null)
        {
            Debug.LogError("FoodList Transform is not found under UICousine.");
            return; // Eğer FoodList bulunamazsa fonksiyonu bitir.
        }

        // FoodList altındaki tüm objeleri temizle
        foreach (Transform child in foodListTransform)
        {
            if (child.gameObject.name != "Food") // Örnek yemek objesini koru
            {
                Destroy(child.gameObject);
            }
        }

        // Her bir food için bir UI elemanı oluştur ve FoodList'e ekle
        foreach (Food food in currentCousine.foods)
        {
            GameObject foodUI = Instantiate(UIFoodTemplate, foodListTransform);
            foodUI.SetActive(true);

            // Yemek bilgilerini UI elementine atayın
            foodUI.GetComponentInChildren<TextMeshProUGUI>().text = food.dishName;
            foodUI.GetComponentInChildren<Image>().sprite = food.icon;
            //foodUI.GetComponentInChildren<Button>().onClick.AddListener(() => SceneHandler.Instance.ChangeScene(currentCousine.cuisineName));

            // Detay görüntüleme butonuna bir event listener ekleyin
            Button detailButton = foodUI.GetComponentInChildren<Button>();
            if (detailButton != null)
            {
                detailButton.onClick.AddListener(() => ShowFoodDetails(food));
            }
        }

        // Diğer UI bileşenlerini ayarla
        UICousine.gameObject.SetActive(true);
        worldMap.gameObject.SetActive(false);
    }

    public void LoadWorldMap()
    {
        worldMap.gameObject.SetActive(true);
        UICousine.gameObject.SetActive(false);
    }
    private void ShowFoodDetails(Food food)
    {
        UIFoodHeader.text = food.dishName;
        UIFoodImage.sprite = food.icon;
        PopulateIngredients(food.ingredients);
        PopulateTools(food.tools);
    }

    private void PopulateIngredients(Ingredient[] ingredients)
    {
        foreach (Ingredient ingredient in ingredients)
        {
            // Her malzeme için UI elementi oluştur ve bilgileri yerleştir
        }
    }

    private void PopulateTools(Tool[] tools)
    {
        foreach (Tool tool in tools)
        {
            // Her alet için UI elementi oluştur ve bilgileri yerleştir
        }
    }
}
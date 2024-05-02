using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenuController : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform UICuisine;
    public TextMeshProUGUI UICuisineHeader;
    public Image UICuisineFlagImage;
    public Image UICuisineChefImage;

    [Header("World Elements")]
    public RectTransform UIWorldMap;
    public Image worldChefImage;
    public Image worldFlagImage;
    public Button worldConfirmButton;
    public TextMeshProUGUI worldDescriptionText;

    [Header("Templates and Lists")]
    public Transform foodListTransform; 
    public GameObject UIFoodTemplate; 

    [Header("Data Elements")]
    private int currentCuisineIndex = -1;
    public Cuisine[] cuisines;
    private Cuisine currentCuisine;
    [Header("Camera")]
    public Camera mainCamera; // Main camera reference

    // Called if a cuisine is selected on world map.
    void Start() {
        mainCamera.backgroundColor = ColorUtility.TryParseHtmlString("#AFDEE4", out Color newColor) ? newColor : Color.white;
        ToggleWorldCuisineInfo(false);
    } 
    public void OnCuisineSelected(int index)
    {
        if(index >= 0 && index < cuisines.Length)
        {
            currentCuisine = cuisines[index];
            UpdateCuisineDisplay();
        }
    }

    public void OnFlagSelected(int index)
    {
        if(index >= 0 && index < cuisines.Length)
        {
            currentCuisineIndex = index;
            UpdateWorldCuisineDisplay(index);
            ToggleWorldCuisineInfo(true);
        }
    }

    // Toggle World cuisine UI
    private void ToggleWorldCuisineInfo(bool visible)
    {
        worldChefImage.gameObject.SetActive(visible);
        worldFlagImage.gameObject.SetActive(visible);
        worldDescriptionText.gameObject.SetActive(visible);
        worldConfirmButton.gameObject.SetActive(visible);
    }

    // Fill World cuisine UI
    private void UpdateWorldCuisineDisplay(int index){
        worldChefImage.sprite = cuisines[index].chefImage;
        worldFlagImage.sprite = cuisines[index].flag;
        worldDescriptionText.text = cuisines[index].description;
    }

    // FoodList page
    public void GoToFoodList()
    {
        currentCuisine = cuisines[currentCuisineIndex];
        UpdateCuisineDisplay();
        UpdateCuisineList();
        LoadCuisineList();
    }

    // Update Cuisine UI.
    private void UpdateCuisineDisplay()
    {
        UICuisineHeader.text = currentCuisine.cuisineName;
        UICuisineFlagImage.sprite = currentCuisine.flag;
        UICuisineChefImage.sprite = currentCuisine.chefImage;
    }

    // Populate Cuisine UI with food templates.
    private void UpdateCuisineList(){

        foreach (Transform child in foodListTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (Food food in currentCuisine.foods)
        {
            GameObject foodUI = Instantiate(UIFoodTemplate, foodListTransform);
            FoodBehaviour foodBehaviour = foodUI.GetComponent<FoodBehaviour>();
            foodBehaviour.Initialize(food);
        }
    }

    public void LoadWorldMap()
    {
        UIWorldMap.gameObject.SetActive(true);
        UICuisine.gameObject.SetActive(false);
        mainCamera.backgroundColor = ColorUtility.TryParseHtmlString("#AFDEE4", out Color newColor) ? newColor : Color.white;
    }

    public void LoadCuisineList(){
        UIWorldMap.gameObject.SetActive(false);
        UICuisine.gameObject.SetActive(true);
        mainCamera.backgroundColor = ColorUtility.TryParseHtmlString("#3E5255", out Color newColor) ? newColor : Color.white;
    }

    public void GoToMainMenu(){
        LevelManager.SceneHandler.LoadScene(GameState.MainMenu);
    }


}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FoodBehaviour : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Elements")]
    public TextMeshProUGUI foodNameText;
    public Image foodImage;
    public Transform ingredientSpriteList;
    public Transform toolSpriteList;
    public Button foodButton;

    [Header("Prefab Templates")]
    public GameObject ImagePrefab;
    public GameSceneData gameSceneData;

    private Food foodData;

    public void Initialize(Food food)
    {
        foodData = food;
        gameSceneData = foodData.sceneData;
        UpdateUI();
    }

    private void UpdateUI()
    {
        foodNameText.text = foodData.dishName;
        foodImage.sprite = foodData.icon;
        PopulateIngredientList();
        PopulateToolList();
    }

    private void PopulateIngredientList()
    {
        
        foreach (Transform child in ingredientSpriteList)
        {
            Destroy(child.gameObject);
        }

        
        foreach (Ingredient ingredient in foodData.ingredients)
        {
            GameObject ingredientIcon = Instantiate(ImagePrefab, ingredientSpriteList);
            ingredientIcon.GetComponent<Image>().sprite = ingredient.ingredientIcon;
        }
    }

    private void PopulateToolList()
    {
        // Clear previous tool icons
        foreach (Transform child in toolSpriteList)
        {
            DestroyImmediate(child.gameObject);
        }

        HashSet<Sprite> addedTools = new HashSet<Sprite>();

        // Create new tool icons
        foreach (Tool tool in foodData.tools)
        {
            if (!addedTools.Contains(tool.toolIcon))
            {
                GameObject toolIcon = Instantiate(ImagePrefab, toolSpriteList);
                toolIcon.GetComponent<Image>().sprite = tool.toolIcon;
                addedTools.Add(tool.toolIcon);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Showing details for: " + foodData.dishName);
        LevelManager.SceneHandler.LoadScene(GameState.Play, gameSceneData);
    }
}

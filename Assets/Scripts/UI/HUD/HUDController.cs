using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Transform toolSpawner;
    public Transform ingredientSpawner;
    public Slider healthSlider;
    public Slider timerSlider;
    public TextMeshProUGUI timerText;
    public GameObject imagePrefab;

    private float maxTime = 180f; 
    private float elapsedTime = 0f;

    void Start()
    {
        timerSlider.maxValue = maxTime;
        timerSlider.value = maxTime;
    }

    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer() 
    {
        if (elapsedTime < maxTime)
        {
            elapsedTime += Time.deltaTime;
            float remainingTime = maxTime - elapsedTime;
            UpdateTimeSlider(remainingTime);
            UpdateTimeText(elapsedTime);
        }
    } 

    private void UpdateTimeSlider(float remainingTime)
    {
        if (timerSlider != null) timerSlider.value = remainingTime;
    }

    private void UpdateTimeText(float remainingTime) 
    {
        timerText.text = FormatTime(remainingTime);
    }
    private string FormatTime(float timeInSeconds)
    {
        int minutes = (int)(timeInSeconds / 60);
        int seconds = (int)(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void UpdateHealth(int currentHealth) => healthSlider.value = currentHealth;

    public void PopulateSpawners()
    {
        // Clear existing items in spawners
        foreach (Transform child in toolSpawner) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in ingredientSpawner) {
            Destroy(child.gameObject);
        }

        Debug.Log("Repopulating...");
        var instructions = LevelManager.InstructionHandler.GetInstructions();
        foreach (var instruction in instructions)
        {
            if (instruction.tool != null)
            {
                InstantiateTool(instruction.tool);
            }
            if (instruction.ingredient != null)
            {
                InstantiateIngredient(instruction.ingredient);
            }
        }
    }

    void InstantiateTool(Tool tool)
    {
        var toolObject = Instantiate(imagePrefab, toolSpawner);
        var image = toolObject.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = tool.toolIcon;
            bool isCollected = LevelManager.InstructionHandler.IsToolCollected(tool);
            image.color = new Color(1f, 1f, 1f, isCollected ? 1f : 0.5f);
            Debug.Log($"Tool: {tool.toolName}, Collected: {isCollected}, Alpha: {image.color.a}");
        }
    }

    void InstantiateIngredient(Ingredient ingredient)
    {
        var ingredientObject = Instantiate(imagePrefab, ingredientSpawner);
        var image = ingredientObject.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = ingredient.ingredientIcon;
            bool isCollected = LevelManager.InstructionHandler.IsIngredientCollected(ingredient);
            image.color = new Color(1f, 1f, 1f, isCollected ? 1f : 0.5f);
            Debug.Log($"Tool: {ingredient.ingredientName}, Collected: {isCollected}, Alpha: {image.color.a}");
        }
    }

    public void RefreshSpawners(){
        PopulateSpawners();
    }
}

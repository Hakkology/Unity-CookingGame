using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIQuestPanelController : MonoBehaviour
{
    [SerializeField] private GUIController GUIController;
    [SerializeField] private GameObject questItemPrefab; // This prefab should have a TextMeshProUGUI component and a Toggle component
    [SerializeField] private Transform questListTransform; // The parent transform where quest items will be instantiated

    private List<GameObject> questItemGameObjects = new List<GameObject>();

    private void Start()
    {
        RefreshQuestList();
        LevelManager.InstructionHandler.InstructionUpdated += RefreshQuestList; // Subscribe to quest updates
    }

    private void OnDestroy()
    {
        LevelManager.InstructionHandler.InstructionUpdated -= RefreshQuestList; // Unsubscribe when destroyed
    }

    private void RefreshQuestList()
    {
        ClearQuestList();
        foreach (var instruction in LevelManager.InstructionHandler.GetInstructions())
        {
            var itemGO = Instantiate(questItemPrefab, questListTransform);
            var itemText = itemGO.GetComponentInChildren<TextMeshProUGUI>();
            var itemToggle = itemGO.GetComponentInChildren<Toggle>();

            itemText.text = instruction.description;
            itemToggle.isOn = LevelManager.InstructionHandler.GetInstructionCompletionStatus(instruction);

            questItemGameObjects.Add(itemGO);
        }
    }

    private void ClearQuestList()
    {
        foreach (var item in questItemGameObjects)
        {
            Destroy(item);
        }
        questItemGameObjects.Clear();
    }

    public void ShowQuestMenu(){
        if (GUIController.IsMenuActive(GUIController.MenuType.Quest))
        {
            GUIController.HideCurrentMenu();
            return;
        }
        LevelManager.SoundManager.PlaySound(SoundEffect.QuestClick);
        GUIController.ShowMenu(GUIController.MenuType.Quest);
    }
    public void HideQuestMenu(){
        GUIController.HideCurrentMenu();
    }
}

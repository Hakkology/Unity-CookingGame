using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    // Handler referansları
    [SerializeField] private ChefCustomizationHandler chefCustomizationHandler;
    [SerializeField] private InstructionHandler instructionHandler;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private AchievementHandler achievementHandler;
    [SerializeField] private List<GameSceneData> gameScenes;
    //[SerializeField] private ThemeHandler themeHandler;

    public static ChefCustomizationHandler ChefCustomizationHandler => Instance.chefCustomizationHandler;
    public static InstructionHandler InstructionHandler => Instance.instructionHandler;
    public static SceneHandler SceneHandler => Instance.sceneHandler;
    public static AchievementHandler AchievementHandler => Instance.achievementHandler;
    //public static ThemeHandler ThemeHandler => Instance.themeHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            AchievementManager.Instance.Initialize(gameScenes);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}

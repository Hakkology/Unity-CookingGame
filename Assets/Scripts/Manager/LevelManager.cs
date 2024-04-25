using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    // Handler referansları
    [SerializeField] private ChefCustomizationHandler chefCustomizationHandler;
    [SerializeField] private InstructionHandler instructionHandler;
    //[SerializeField] private ThemeHandler themeHandler;

    public static ChefCustomizationHandler ChefCustomizationHandler => Instance.chefCustomizationHandler;
    public static InstructionHandler InstructionHandler => Instance.instructionHandler;
    //public static ThemeHandler ThemeHandler => Instance.themeHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    
}

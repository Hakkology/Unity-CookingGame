using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChefCustomizationHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starText;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        UpdateStarDisplay();
        UpdateCoinDisplay();
        //purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }

    private void UpdateStarDisplay()
    {
        int totalStars = LevelManager.CurrencyManager.GetTotalStars();
        starText.text = totalStars.ToString();
    }

    private void UpdateCoinDisplay(){
        int totalCoins = LevelManager.CurrencyManager.GetTotalCoins();
        coinText.text = totalCoins.ToString();
    }

    // private void OnPurchaseButtonClicked()
    // {
    //     int cost = 10; // Example cost
    //     if (CurrencyManager.Instance.SpendStars(cost))
    //     {
    //         Debug.Log("Purchase successful!");
    //         UpdateStarDisplay();
    //     }
    //     else
    //     {
    //         Debug.Log("Not enough stars for purchase.");
    //     }
    // }
}

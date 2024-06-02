using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinPackageBehaviour : MonoBehaviour
{
    public Image packageImageDisplay;
    public TextMeshProUGUI priceDisplay;
    public TextMeshProUGUI coinAmountDisplay;
    public TextMeshProUGUI titleDisplay;

    private CoinPackage coinPackage; // Bu UI elementi tarafından temsil edilen CoinPackage

    public void SetCoinPackage(CoinPackage package)
    {
        coinPackage = package;
        InitializePackage();
    }

    private void InitializePackage()
    {
        if (coinPackage != null)
        {
            titleDisplay.text = coinPackage.description;
            packageImageDisplay.sprite = coinPackage.packageImage;
            priceDisplay.text = "$" + coinPackage.priceUSD.ToString("F2");
            coinAmountDisplay.text = coinPackage.coinAmount.ToString();
        }
        else
        {
            Debug.LogWarning("CoinPackage is not set for " + gameObject.name);
        }
    }

    public void Purchase()
    {
        // Burası IAP entegrasyonu için bir placeholder
        Debug.Log("Attempting to purchase: " + coinPackage.coinAmount + " Coins for $" + coinPackage.priceUSD);
        // Gerçek satın alma işlemi burada gerçekleştirilecek
    }
}

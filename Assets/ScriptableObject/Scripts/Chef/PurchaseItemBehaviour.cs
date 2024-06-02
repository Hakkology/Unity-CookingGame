using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItemBehaviour : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI starText;
    public TextMeshProUGUI coinText;
    public Button purchaseStarButton;
    public Button purchaseCoinButton;

    private string itemName;
    

    public void Initialize(ChefHatData hatData)
    {
        itemName = hatData.chefHatName;
        itemImage.sprite = hatData.chefHatIcon;
        starText.text = hatData.starCost.ToString();
        coinText.text = hatData.coinCost.ToString();

        if (LevelManager.PurchaseManager.IsItemPurchased(itemName))
            DisableButtons();
        
        else
        {
            purchaseStarButton.onClick.AddListener(() => PurchaseWithStars(hatData.starCost, itemName));
            purchaseCoinButton.onClick.AddListener(() => PurchaseWithCoins(hatData.coinCost, itemName));
        }
    }
    public void Initialize(ChefAccessoryData accessoryData)
    {
        itemName = accessoryData.chefAccessoryName;
        itemImage.sprite = accessoryData.chefAccessoryIcon;
        starText.text = accessoryData.starCost.ToString();
        coinText.text = accessoryData.coinCost.ToString();

        if (LevelManager.PurchaseManager.IsItemPurchased(itemName))
        {
            DisableButtons();
        }
        else
        {
            purchaseStarButton.onClick.AddListener(() => PurchaseWithStars(accessoryData.starCost, itemName));
            purchaseCoinButton.onClick.AddListener(() => PurchaseWithCoins(accessoryData.coinCost, itemName));
        }
    }
    public void Initialize(ChefMaterialData materialData)
    {
        itemName = materialData.chefMaterialName;
        itemImage.sprite = materialData.chefMaterialIcon;
        starText.text = materialData.starCost.ToString();
        coinText.text = materialData.coinCost.ToString();

        if (LevelManager.PurchaseManager.IsItemPurchased(itemName))
        {
            DisableButtons();
        }
        else
        {
            purchaseStarButton.onClick.AddListener(() => PurchaseWithStars(materialData.starCost, itemName));
            purchaseCoinButton.onClick.AddListener(() => PurchaseWithCoins(materialData.coinCost, itemName));
        }
    }


    private void PurchaseWithStars(int starCost, string itemName)
    {
        if (LevelManager.CurrencyManager.SpendStars(starCost))
        {
            LevelManager.PurchaseManager.MarkItemAsPurchased(itemName);
            DisableButtons();
            Debug.Log("Purchased " + itemName + " with stars.");
        }
        else
        {
            Debug.Log("Not enough stars to purchase " + itemName);
        }
    }

    private void PurchaseWithCoins(int coinCost, string itemName)
    {
        if (LevelManager.CurrencyManager.SpendCoins(coinCost))
        {
            LevelManager.PurchaseManager.MarkItemAsPurchased(itemName);
            DisableButtons();
            Debug.Log("Purchased " + itemName + " with coins.");
        }
        else
        {
            Debug.Log("Not enough coins to purchase " + itemName);
        }
    }

    private void DisableButtons()
    {
        purchaseStarButton.interactable = false;
        purchaseCoinButton.interactable = false;
    }
}
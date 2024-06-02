using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing() 
    {
        if (IsInitialized())
            return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct("coin_package_150", UnityEngine.Purchasing.ProductType.Consumable);
        builder.AddProduct("coin_package_500", UnityEngine.Purchasing.ProductType.Consumable);
        builder.AddProduct("coin_package_2000", UnityEngine.Purchasing.ProductType.Consumable);
        builder.AddProduct("coin_package_5000", UnityEngine.Purchasing.ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyProductID(string productId)
    {
        if (!IsInitialized())
            return;

        m_StoreController.InitiatePurchase(productId);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
    {
        if (String.Equals(args.purchasedProduct.definition.id, "coin_package_150", StringComparison.Ordinal))
        {
            Debug.Log("150 coins purchased");
            LevelManager.CurrencyManager.AddCoins(150);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "coin_package_500", StringComparison.Ordinal))
        {
            Debug.Log("500 coins purchased");
            LevelManager.CurrencyManager.AddCoins(500);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "coin_package_2000", StringComparison.Ordinal))
        {
            Debug.Log("2000 coins purchased");
            LevelManager.CurrencyManager.AddCoins(2000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "coin_package_5000", StringComparison.Ordinal))
        {
            Debug.Log("5000 coins purchased");
            LevelManager.CurrencyManager.AddCoins(5000);
        }
        else 
        {
            Debug.Log("Purchase Unrecognized");
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // Satın alma başarısızlığının detaylarını loglayın
        Debug.LogError($"Purchase failed: {product.definition.id}. Reason: {failureReason}");

        //ShowErrorMessage($"Purchase failed: {product.definition.storeSpecificId}. Please try again.");
    }


    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        // Başlatma başarısızlığının detaylarını loglayın
        Debug.LogError($"IAP Initialization failed: Reason: {error}. Message: {message}");

        //ShowErrorMessage("Failed to initialize purchases. Please check your connection and restart the app.");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        // Satın alma başarısızlığının detaylarını loglayın
        Debug.LogError($"Purchase failed: {product.definition.id}. Reason: {failureDescription.reason} - Message: {failureDescription.message}");

        //ShowErrorMessage($"Purchase of {product.definition.storeSpecificId} failed: {failureDescription.message}. Please try again.");
    }
}

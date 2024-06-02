// using UnityEngine;
// using UnityEngine.Purchasing;

// public class IAPManager : MonoBehaviour, IStoreListener
// {
//     private static IStoreController m_StoreController;
//     private static IExtensionProvider m_StoreExtensionProvider;

//     void Start()
//     {
//         if (m_StoreController == null)
//         {
//             InitializePurchasing();
//         }
//     }

//     public void InitializePurchasing() 
//     {
//         if (IsInitialized())
//             return;

//         var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
//         builder.AddProduct("coin_package_150", ProductType.Consumable);
//         builder.AddProduct("coin_package_500", ProductType.Consumable);
//         builder.AddProduct("coin_package_2000", ProductType.Consumable);

//         UnityPurchasing.Initialize(this, builder);
//     }

//     private bool IsInitialized()
//     {
//         return m_StoreController != null && m_StoreExtensionProvider != null;
//     }

//     public void BuyProductID(string productId)
//     {
//         if (!IsInitialized())
//             return;

//         m_StoreController.InitiatePurchase(productId);
//     }

//     public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//     {
//         m_StoreController = controller;
//         m_StoreExtensionProvider = extensions;
//     }

//     public void OnInitializeFailed(InitializationFailureReason error)
//     {
//         Debug.LogError("OnInitializeFailed InitializationFailureReason:" + error);
//     }

//     public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
//     {
//         if (String.Equals(args.purchasedProduct.definition.id, "coin_package_150", StringComparison.Ordinal))
//         {
//             Debug.Log("150 coins purchased");
//             // Burada coin eklemek için CurrencyManager kullanın
//         }
//         else if (String.Equals(args.purchasedProduct.definition.id, "coin_package_500", StringComparison.Ordinal))
//         {
//             Debug.Log("500 coins purchased");
//             // Burada coin eklemek için CurrencyManager kullanın
//         }
//         else if (String.Equals(args.purchasedProduct.definition.id, "coin_package_2000", StringComparison.Ordinal))
//         {
//             Debug.Log("2000 coins purchased");
//             // Burada coin eklemek için CurrencyManager kullanın
//         }
//         else 
//         {
//             Debug.Log("Purchase Unrecognized");
//         }
//         return PurchaseProcessingResult.Complete;
//     }

//     public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//     {
//         Debug.LogError("OnPurchaseFailed PurchaseFailureReason:" + failureReason);
//     }
// }

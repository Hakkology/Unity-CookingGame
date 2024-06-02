using UnityEngine;

public class ChefCustomizationPurchaseController : MonoBehaviour
{
    public ChefHatDataList chefHatDataList;
    public ChefAccessoryDataList chefAccessoryDataList;
    public ChefMaterialDataList chefMaterialDataList;
    public GameObject purchaseItemPrefab;
    public Transform hatContent;
    public Transform skinContent;
    public Transform accessoryContent;

    private void Start()
    {
        PopulateHatWheel();
        PopulateAccessoryWheel();
        PopulateSkinWheel();
    }

    private void PopulateHatWheel()
    {
        foreach (var hatData in chefHatDataList.chefHatDataList)
        {
            GameObject newItem = Instantiate(purchaseItemPrefab, hatContent);
            PurchaseItemBehaviour purchaseItemBehaviour = newItem.GetComponent<PurchaseItemBehaviour>();
            if (purchaseItemBehaviour != null)
            {
                purchaseItemBehaviour.Initialize(hatData);
            }
        }
    }
    private void PopulateAccessoryWheel()
    {
        foreach (var accessoryData in chefAccessoryDataList.chefAccessoryDataList)
        {
            GameObject newItem = Instantiate(purchaseItemPrefab, accessoryContent);
            PurchaseItemBehaviour purchaseItemBehaviour = newItem.GetComponent<PurchaseItemBehaviour>();
            if (purchaseItemBehaviour != null)
            {
                purchaseItemBehaviour.Initialize(accessoryData);
            }
        }
    }
    private void PopulateSkinWheel()
    {
        foreach (var skinData in chefMaterialDataList.chefMaterialDataList)
        {
            GameObject newItem = Instantiate(purchaseItemPrefab, skinContent);
            PurchaseItemBehaviour purchaseItemBehaviour = newItem.GetComponent<PurchaseItemBehaviour>();
            if (purchaseItemBehaviour != null)
            {
                purchaseItemBehaviour.Initialize(skinData);
            }
        }
    }
}

using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public ChefHatDataList chefHatDataList;
    public ChefAccessoryDataList chefAccessoryDataList;
    public ChefMaterialDataList chefMaterialDataList;
    private void Start() => InitializePlayerPrefs();
    private void InitializePlayerPrefs()
    {
        // Chef Hat Data
        foreach (var hatData in chefHatDataList.chefHatDataList)
        {
            if (!PlayerPrefs.HasKey(hatData.chefHatName))
            {
                PlayerPrefs.SetInt(hatData.chefHatName, 0); // 0 means not purchased
            }
        }
        // Chef Accessor Data
        foreach (var accessoryData in chefAccessoryDataList.chefAccessoryDataList)
        {
            if (!PlayerPrefs.HasKey(accessoryData.chefAccessoryName)){
                PlayerPrefs.SetInt(accessoryData.chefAccessoryName, 0); // 0 means not purchased
            }
        }
        // Chef Material Data
        foreach (var materialData in chefMaterialDataList.chefMaterialDataList)
        {
            if (!PlayerPrefs.HasKey(materialData.chefMaterialName)){
                PlayerPrefs.SetInt(materialData.chefMaterialName, 0); // 0 means not purchased
            }
        }
        PlayerPrefs.Save();
    }
    public bool IsItemPurchased(string itemName) => PlayerPrefs.GetInt(itemName, 0) == 1;
    public void MarkItemAsPurchased(string itemName)
    {
        PlayerPrefs.SetInt(itemName, 1); // 1 means purchased
        PlayerPrefs.Save();
    }
}

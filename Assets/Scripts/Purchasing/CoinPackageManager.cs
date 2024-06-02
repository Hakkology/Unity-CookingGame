using UnityEngine;

public class CoinPackageManager : MonoBehaviour
{
    public CoinPackage[] coinPackages; // Tüm coin paketlerini tutan dizi
    public GameObject coinPackagePrefab; // Coin package UI prefab
    public Transform contentArea; // UI'da coin paketlerinin yerleştirileceği alan

    private void Start()
    {
        InitializePackages();
    }

    private void InitializePackages()
    {
        foreach (CoinPackage package in coinPackages)
        {
            GameObject packageInstance = Instantiate(coinPackagePrefab, contentArea);
            CoinPackageBehaviour packageBehaviour = packageInstance.GetComponent<CoinPackageBehaviour>();
            if (packageBehaviour != null)
            {
                packageBehaviour.SetCoinPackage(package);
            }
        }
    }

    public CoinPackage GetCoinPackage(int index)
    {
        if (index >= 0 && index < coinPackages.Length)
            return coinPackages[index];
        return null;
    }
}

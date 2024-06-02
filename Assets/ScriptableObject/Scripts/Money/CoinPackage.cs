using UnityEngine;

[CreateAssetMenu(fileName = "New CoinPackage", menuName = "Coin System/Coin Package")]
public class CoinPackage : ScriptableObject
{
    public int coinAmount; 
    public float priceUSD; 
    public Sprite packageImage; 
    public string description; 
}

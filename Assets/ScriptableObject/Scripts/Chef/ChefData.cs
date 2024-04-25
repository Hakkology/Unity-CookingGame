using UnityEngine;

[CreateAssetMenu(fileName = "ChefData", menuName = "Customization/ChefData")]
public class ChefData : ScriptableObject
{
    public string chefName;
    public Sprite chefFlagIcon;
    public Sprite chefIcon;
}

public enum Country
{
    China, France, Greece, India, Italy, Mexico, Turkey
}

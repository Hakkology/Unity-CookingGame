using UnityEngine;
[CreateAssetMenu(fileName = "Cuisine", menuName = "Cuisine/Tools")]
public class Tool : Pickup
{
    public string toolName;
    public Sprite toolIcon;
    public GameObject toolObject;
    public SoundEffect toolSound;
}
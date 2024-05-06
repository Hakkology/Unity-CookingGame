using UnityEngine;
[CreateAssetMenu(fileName = "Cuisine", menuName = "Cuisine/Tools")]
public class Tool : ScriptableObject
{
    public string toolName;
    public Sprite toolIcon;
    public GameObject toolObject;
}
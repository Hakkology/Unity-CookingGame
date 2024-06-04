using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Audio/SoundData", order = 1)]
public class SoundData : ScriptableObject
{
    public SoundEffect soundEffect;
    public AudioClip audioClip;
    public bool loop; 
}

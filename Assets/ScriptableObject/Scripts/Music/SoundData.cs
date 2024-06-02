using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Audio/SoundData", order = 1)]
public class SoundData : ScriptableObject
{
    public string soundName;
    public AudioClip audioClip;
}

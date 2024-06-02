using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicList", menuName = "Music/Music List")]
public class MusicList : ScriptableObject
{
    public Kitchen kitchenType;
    public List<AudioClip> musicClips;
}

[System.Serializable]
public class KitchenMusicList
{
    public Kitchen kitchen;
    public MusicList musicList;
}

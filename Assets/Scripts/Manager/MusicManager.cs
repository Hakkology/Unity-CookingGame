using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public List<KitchenMusicList> musicListsEditable = new List<KitchenMusicList>();
    private Dictionary<Kitchen, MusicList> musicLists = new Dictionary<Kitchen, MusicList>();

    public AudioSource musicSource;

    public float musicChangeTimer;
    public float musicVolumeChangeTimer;

    public float musicMaxVolume;
    private float changeTimer;

    private bool isChanging;
    private GameSceneData currentGameSceneData;

    private void Start() {
        AssignMusicList();
        LevelManager.SceneHandler.OnPlayScene += UpdateMusicBasedOnKitchen;
        SetNewMusic(currentGameSceneData);
    } 
    private void OnDestroy() => LevelManager.SceneHandler.OnPlayScene -= UpdateMusicBasedOnKitchen;
    
    void Update()
    {
        if (!isChanging)
            changeTimer += Time.deltaTime;
        
        if (changeTimer >= musicChangeTimer)
            ChangeMusic();
    }

    public void ChangeMusic()
    {
        isChanging = true;
        changeTimer = 0;
        StartCoroutine(ChangeMusicRoutine());
    }

    private void AssignMusicList()
    {
        foreach (var item in musicListsEditable) {
            if (!musicLists.ContainsKey(item.kitchen)) {
                musicLists.Add(item.kitchen, item.musicList);
            }
        }
    }

    private IEnumerator ChangeMusicRoutine()
    {
        if (currentGameSceneData != null && musicSource.clip != null)
        {
            Kitchen currentKitchenType = musicLists.FirstOrDefault(x => x.Value.musicClips.Contains(musicSource.clip)).Key;
            if (currentGameSceneData.kitchenType == currentKitchenType)
                yield break;
        }
        yield return StartCoroutine(DecreaseVolume());
        SetNewMusic(currentGameSceneData);  // Use cached game scene data
        yield return StartCoroutine(IncreaseVolume());
    }

    public void SetNewMusic(GameSceneData gameSceneData)
    {
        musicSource.Stop();
        Kitchen kitchenType = gameSceneData != null ? gameSceneData.kitchenType : Kitchen.Default;
        MusicList currentMusicList = musicLists[kitchenType];
        musicSource.clip = currentMusicList.musicClips[Random.Range(0, currentMusicList.musicClips.Count)];
        musicSource.Play();
    }

    private void UpdateMusicBasedOnKitchen(GameSceneData gameSceneData)
    {
        currentGameSceneData = gameSceneData;
        SetNewMusic(gameSceneData);
    }

    IEnumerator IncreaseVolume()
    {
        while (musicSource.volume < musicMaxVolume)
        {
            musicSource.volume += (musicMaxVolume / musicVolumeChangeTimer) * Time.deltaTime;
            yield return null;
        }

        isChanging = false;
    }

    IEnumerator DecreaseVolume()
    {
        while (musicSource.volume != 0)
        {
            musicSource.volume -= (musicMaxVolume / musicVolumeChangeTimer) * Time.deltaTime;
            yield return null;
        }
    }
}
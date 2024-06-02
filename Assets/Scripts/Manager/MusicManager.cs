using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public Dictionary<Kitchen, MusicList> musicLists;
    public AudioSource musicSource;

    public float musicChangeTimer;
    public float musicVolumeChangeTimer;

    public float musicMaxVolume;
    private float changeTimer;

    private bool isChanging;
    private GameSceneData currentGameSceneData;

    private void Start() => LevelManager.SceneHandler.OnPlayScene += UpdateMusicBasedOnKitchen;
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

    private IEnumerator ChangeMusicRoutine()
    {
        yield return StartCoroutine(DecreaseVolume());
        SetNewMusic(currentGameSceneData);  // Use cached game scene data
        yield return StartCoroutine(IncreaseVolume());
    }

    public void SetNewMusic(GameSceneData gameSceneData)
    {
        musicSource.Stop();
        MusicList currentMusicList = musicLists[gameSceneData.kitchenType];
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
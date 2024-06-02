using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public List<AudioClip> musicClips;
    public AudioSource musicSource;

    public float musicChangeTimer;
    public float musicVolumeChangeTimer;

    public float musicMaxVolume;
    private float changeTimer;

    private bool isChanging;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Singleton pattern
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() =>
        SetNewMusic();


    /*
     
     volume 1 den 0 a düşürücek
    müziği değiştiricek
    volume 0 dan 1 e yükseltecek
     
     */
    // Update is called once per frame
    void Update()
    {
        if (!isChanging)
            changeTimer += Time.deltaTime;
        
        else
            return;
        

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
        SetNewMusic();
        yield return StartCoroutine(IncreaseVolume());
    }

    public void SetNewMusic()
    {
        musicSource.Stop();
        musicSource.clip = musicClips[Random.Range(0, musicClips.Count)];
        musicSource.Play();
    }

    IEnumerator IncreaseVolume()
    {
        while (musicSource.volume < musicMaxVolume)
        {
            musicSource.volume += (musicMaxVolume / musicVolumeChangeTimer) * Time.deltaTime;
            yield return null;
        }

        if (musicSource.volume >= musicMaxVolume)
        {
            isChanging = false;
        }
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
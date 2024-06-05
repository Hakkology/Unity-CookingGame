using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;

    }
    private void OnVideoEnd(VideoPlayer vp) => SceneManager.LoadScene("MainMenuScene");
    private void OnDestroy() => videoPlayer.loopPointReached -= OnVideoEnd;

}
